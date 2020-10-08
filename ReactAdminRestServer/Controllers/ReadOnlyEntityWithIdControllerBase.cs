using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ReactAdminRestServer.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ReactAdminRestServer.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class ReadOnlyEntityWithIdControllerBase<TEntity,  TReadModel, TIndentifier> : BaseEFCoreAPIController
        where TEntity : class, IHaveIdentifier<TIndentifier>
        
    {
        public ReadOnlyEntityWithIdControllerBase(
            DbContext     dbContext, 
            IMapper       mapper,
            string        identityPropertyName,
            CacheInterval cacheInterval        = CacheInterval.OneMinute,
            int           pageSize             = 50
            ) : base(dbContext, mapper, identityPropertyName, cacheInterval, pageSize) { }
       

        #region Expressions and filter delegates

        /// <summary>
        /// Defines the Linq expression to compare against an entity identifier. Must define.
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        protected abstract Expression<Func<TEntity, bool>> GetIdentifierEqualityFn(TIndentifier o);

        
        /// <summary>
        /// When you need to include related entities on the detail response, you can add them via this IQueryable by overriding this function 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> IncludeTheseForDetailResponse(IQueryable<TEntity> queryable) => queryable;
        
        /// <summary>
        /// When you need to include related entities on the list response, you can add them via this IQueryable by overriding this function 
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> IncludeTheseForListResponse(IQueryable<TEntity> queryable) => queryable;



        /// <summary>
        /// IQueryable<TEntity> Delegated means to filter more specifically, for example to only include results by some criteria based on an Included child entity.
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> WithFilters(IQueryable<TEntity> queryable) => queryable;
        
        /// <summary>
        /// Wraps the identifier function so that it can be added to, in the case of multiple IDs to match, or multiple filter critiera.
        /// </summary>
        /// <param name="expressionStarter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        protected virtual Expression<Func<TEntity, bool>> IdentityExpressionFn(ExpressionStarter<TEntity> expressionStarter, JToken token)  
        {
            return expressionStarter.Or(GetIdentifierEqualityFn(token.Value<TIndentifier>()));
        }
       
        #endregion Expressions and filter delegates
        
        #region Model interface
        /// <summary>
        /// Get a TReadModel by its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<TReadModel> ReadModel(TIndentifier id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var model = await IncludeTheseForDetailResponse(
                DataContext.Set<TEntity>()
                .AsNoTracking()
                )

                .Where(GetIdentifierEqualityFn(id))
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return model;
        }

        /// <summary>
        /// Get a paged list of ReadModels by searching by the filter dialect. 
        /// </summary>
        /// <param name="filter">the data provider's filter object</param>
        /// <param name="ranges">a string in the format of "[0,24]"</param>
        /// <param name="sorts">a string in the format of "['propetyName','asc']"</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<IPagedList<TReadModel>> QueryModel( string filter = null, string ranges = null, string sorts = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var queryProps = new QueryDialectProperties<TEntity>(IdentityExpressionFn, PageSize, IdentityPropertyName, filter, ranges, sorts);
            return await PerformQueryModel<TEntity, TIndentifier, TReadModel>(queryProps, IncludeTheseForListResponse, WithFilters, cancellationToken);
        }
        #endregion Model interface

        #region Http API / boilerplate minimization
        
        [HttpGet("{id}")]
        public async Task<ActionResult<TReadModel>> Get(CancellationToken cancellationToken, TIndentifier id) 
        {
            var readModel = await ReadModel(id, cancellationToken);

            if (readModel == null)
                return NotFound();

            return readModel;
        }

            
        //GET http://my.api.url/posts?filter={"author_id":345}
        //GET http://my.api.url/posts?filter={"id":[123,456,789]}
        //GET http://my.api.url/posts?sort=["title","ASC"]&range=[0, 24]&filter={"title":"bar"}
        [HttpGet("")]
        public async Task<IReadOnlyList<TReadModel>> List(CancellationToken cancellationToken, 
            [FromQueryAttribute] string filter = null, 
            [FromQueryAttribute] string range = null, 
            [FromQueryAttribute] string sort = null)
        {
            var result = await QueryModel(filter, range, sort, cancellationToken);
            return ToListResult<TEntity, TReadModel>(result);
        }

        #endregion Http API / boilerplate minimization

    }
}
