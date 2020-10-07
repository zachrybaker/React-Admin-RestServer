using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ReactAdminRestServer.Common.Definitions;
using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ReactAdminRestServer.Common.Controllers
{
    /// <summary>
    /// A Guid-based identity controller.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TReadModel"></typeparam>
    /// <typeparam name="TCreateModel"></typeparam>
    /// <typeparam name="TUpdateModel"></typeparam>
    [ApiController]
    [Produces("application/json")]
    public abstract class EntityWithGuidIdController<TEntity,  TReadModel, TCreateModel, TUpdateModel> : 
        EntityWithIdControllerBase<TEntity,  TReadModel, TCreateModel, TUpdateModel, Guid>
        where TEntity : class, IHaveIdentifier<Guid>, new()
        
    {
        public EntityWithGuidIdController(
            DbContext     dbContext, 
            IMapper       mapper,
            string        identityPropertyName,
            CacheInterval cacheInterval         = CacheInterval.ThirtySeconds,
            int           pageSize              = 50
            ) : base(dbContext, mapper, identityPropertyName, cacheInterval, pageSize) { }

        #region Expressions and filter delegates
        /// <summary>
        /// Defines the Linq expression to compare against an entity identifier. 
        /// If you need to use a primay key other than "Id", then you must override this.
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        protected override Expression<Func<TEntity, bool>> GetIdentifierEqualityFn(Guid g)  => (x => x.Id == g);

        /// <summary>
        /// Wraps the identifier function so that it can be added to, in the case of multiple IDs to match, or multiple filter critiera.
        /// </summary>
        /// <param name="expressionStarter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        protected override Expression<Func<TEntity, bool>> IdentityExpressionFn(ExpressionStarter<TEntity> expressionStarter, JToken token)  
        {
            if (Guid.TryParse((string)token, out Guid g))
                return expressionStarter.Or(GetIdentifierEqualityFn(g));
        
            Debug.WriteLine((string)token + " Is NOT a valid Guid.  why was it sent?");
            return null;
        }
        #endregion Expressions and filter delegates
    }
}
