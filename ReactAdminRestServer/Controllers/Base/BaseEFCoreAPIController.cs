using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactAdminRestServer.Common.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReactAdminRestServer.Common.Controllers
{
     public delegate IQueryable<TEntity> WithFilter<TEntity, TIndentifier>( IQueryable<TEntity> queryable) 
        where TEntity : class, IHaveIdentifier<TIndentifier>; 

    /// <summary>
    /// Encapsulates the common and EFCore-facing operations once the generics have been resolved.
    /// </summary>
    public class BaseEFCoreAPIController : ControllerBase
    {
        /// <summary>
        /// Defines the name of the primary key (singular, not compound) i.e. "Id".  
        /// Used only in getMany which will send in an array of identifiers. 
        /// Avoids a factory and a factory builder. Rquired.
        /// </summary>
        protected static string          IdentityPropertyName;

        public static int                PageSize             { get; set; }        = 50;
        protected     CacheInterval      CacheInterval        { get; private set;} = CacheInterval.ThirtySeconds;
        protected     DbContext          DataContext          { get; }
        protected     IMapper            Mapper               { get; }

        public BaseEFCoreAPIController(
            DbContext     dataContext,
            IMapper       mapper, 
            string        identityPropertyName,
            CacheInterval cacheInterval = CacheInterval.ThirtySeconds,
            int           pageSize      = 50)
        {
            DataContext          = dataContext;
            Mapper               = mapper;
            IdentityPropertyName = identityPropertyName;
            CacheInterval        = cacheInterval;
            PageSize             = pageSize;
        }

        /// <summary>
        /// Perform a filtered search and return a page of results
        /// </summary>
        /// <typeparam name="TEntity">The entity being queried</typeparam>
        /// <typeparam name="TIndentifier">The type of identifier for said entity</typeparam>
        /// <typeparam name="TReadModel">The model we will map the entity to to return</typeparam>
        /// <param name="queryProps">The data structure representing the query to execute</param>
        /// <param name="withFilter">IQueryable<TEntity> Means to filter more specifically, for example to only include results by some criteria based on an Included child entity.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<IPagedList<TReadModel>> PerformQueryModel<TEntity,TIndentifier, TReadModel>(
            QueryDialectProperties<TEntity>   queryProps,
            WithFilter<TEntity, TIndentifier> withIncludes,
            WithFilter<TEntity, TIndentifier> withFilters,
            CancellationToken                 cancellationToken = default(CancellationToken))
             where TEntity : class, IHaveIdentifier<TIndentifier>
        {
            var dbSet = DataContext
                .Set<TEntity>();

            var query = dbSet.AsNoTracking();
            
            query = withIncludes(query);

            if (queryProps.Predicate != null)
                query = query.Where(queryProps.Predicate);

            // Apply a filtering function.  Might just be a pass-thru.
            query = withFilters(query);

            if (queryProps.Filters != null && queryProps.Filters.Count > 0)
                foreach(var filter in queryProps.Filters)
                    query = query.Where(filter.Key, filter.Value);

            
            // Too bad we can't do this to get count in same query 
            // (https://entityframeworkcore.com/knowledge-base/36478179/ef-core-paging--select-total-count-in-same-query)
            // It isn't compatible with the dynamic link.  
            // And moving he grouping lower results in client-side evaluation, which is a no-no.
            int count = await query.CountAsync();

            if(!string.IsNullOrEmpty(queryProps.OrderField))
                query = query.OrderBy(
                    $"{queryProps.OrderField} {(queryProps.OrderDirection.ToLower() == "desc" ? "descending" : "")}");

            if (queryProps.StartIndex > 0)
                query = query.Skip(queryProps.StartIndex);
            
            query = query.Take(queryProps.EndIndex - queryProps.StartIndex + 1);

            var results = await query
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new PagedList<TReadModel>(results, count, queryProps.StartIndex, queryProps.EndIndex);
        }

        protected virtual IReadOnlyList<TReadModel> ToListResult<TEntity, TReadModel>(IPagedList<TReadModel> result)
        {
            // The simple REST data provider expects the API to include a Content-Range header in the response to getList calls. 
            // The value must be the total number of resources in the collection. 
            // This allows react-admin to know how many pages of resources there are in total, and build the pagination controls.
            string contentRangeHeader =  
                $"{(typeof(TEntity).Name.ToLower())} {result.StartIndex}-{result.EndIndex}/{result.TotalCount}";

            HttpContext.Response.Headers.Add("Content-Range", contentRangeHeader);

            // Add caching indicators.
            HttpContext.AddCacheValidUntilHeader(CacheInterval);

            return result.List;
        }
    }
}
