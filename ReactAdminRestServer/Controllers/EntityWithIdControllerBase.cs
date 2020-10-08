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
    public abstract class EntityWithIdControllerBase<TEntity,  TReadModel, TCreateModel, TUpdateModel, TIndentifier> : BaseEFCoreAPIController
        where TEntity : class, IHaveIdentifier<TIndentifier>
    {
        public EntityWithIdControllerBase(
            DbContext     dbContext, 
            IMapper       mapper,
            string        identityPropertyName,
            CacheInterval cacheInterval         = CacheInterval.ThirtySeconds,
            int           pageSize              = 50
            ) : base(dbContext, mapper, identityPropertyName, cacheInterval, pageSize) { }

        #region Expressions and filter delegates
        
        /// <summary>
        /// Defines the Linq expression to compare against an entity identifier. 
        /// You Must define.
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
                .AsNoTracking())

                .Where(GetIdentifierEqualityFn(id))
                .ProjectTo<TReadModel>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return model;
        }

        /// <summary>
        /// Create an entity by its CreateModel
        /// </summary>
        /// <param name="createModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<TReadModel> CreateModel(TCreateModel createModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Map a new entity from the DTO/model.
            var entity = Mapper.Map<TEntity>(createModel);

            // Add to data context, entity.id should be generated in the process.
            await DataContext
                .Set<TEntity>()
                .AddAsync(entity, cancellationToken);

            // Save the context changes to database.
            await DataContext
                .SaveChangesAsync(cancellationToken);

            // Finally, grab the result and return it as a read model.
            return await ReadModel(entity.Id, cancellationToken);
        }

        /// <summary>
        /// Update an entity by its UpdateModel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<TReadModel> UpdateModel(TIndentifier id, TUpdateModel updateModel, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Retreive the context entity to update.
            var entity = await DataContext
                .Set<TEntity>()
                .Where(GetIdentifierEqualityFn(id))
                .FirstOrDefaultAsync();

            if (entity == null)
                return default(TReadModel);

            // Copy the updates from model to entity.
            Mapper.Map(updateModel, entity);

            // Commit those updates updates to the database.
            await DataContext
                .SaveChangesAsync(cancellationToken);

            // Return the read model.
            return await ReadModel(id, cancellationToken);
        }

        /// <summary>
        /// Delete an entity by its identifier and return the deleted result.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual async Task<TReadModel> DeleteModel(TIndentifier id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var dbSet = DataContext
                .Set<TEntity>();

            // Retreive the entity to delete from the context.
            var entity = await dbSet
                .Where(GetIdentifierEqualityFn(id))
                .FirstOrDefaultAsync(); 

            if (entity == null)
                return default(TReadModel);

            // Map to the read model.
            var readModel = await ReadModel(id, cancellationToken);

            // Delete entry in the context.
            dbSet.Remove(entity);

            // Save the context's delete to the database.
            await DataContext
                .SaveChangesAsync(cancellationToken);

            return readModel;
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
        public async virtual Task<ActionResult<TReadModel>> Get(CancellationToken cancellationToken, TIndentifier id) 
        {
            var readModel = await ReadModel(id, cancellationToken);

            if (readModel == null)
                return NotFound();

            return readModel;
        }

     
        [HttpPut("{id}")]
        public async virtual Task<ActionResult<TReadModel>> Update(CancellationToken cancellationToken, TIndentifier id, TUpdateModel updateModel)
        {
            var readModel = await UpdateModel(id, updateModel, cancellationToken);

            if (readModel == null)
                return NotFound();

            return readModel;
        }

        [HttpDelete("{id}")]
        public async virtual Task<ActionResult<TReadModel>> Delete(CancellationToken cancellationToken, TIndentifier id)
        {
            var readModel = await DeleteModel(id, cancellationToken);
            if (readModel == null)
                return NotFound();

            return readModel;
        }

        
        //GET http://my.api.url/posts?filter={"author_id":345}
        //GET http://my.api.url/posts?filter={"id":[123,456,789]}
        //GET http://my.api.url/posts?sort=["title","ASC"]&range=[0, 24]&filter={"title":"bar"}
        [HttpGet("")]
        public async virtual Task<IReadOnlyList<TReadModel>> List(CancellationToken cancellationToken, 
            [FromQuery] string filter = null, 
            [FromQuery] string range  = null, 
            [FromQuery] string sort   = null)
        {
            var result = await QueryModel(filter, range, sort, cancellationToken);
            return ToListResult<TEntity, TReadModel>(result);
        }

        [HttpPost("")]
        public async virtual Task<ActionResult<TReadModel>> Create(CancellationToken cancellationToken, TCreateModel createModel)
        {
            var readModel = await CreateModel(createModel, cancellationToken);

            return readModel;
        }

        #endregion Http API / boilerplate minimization
    }
}
