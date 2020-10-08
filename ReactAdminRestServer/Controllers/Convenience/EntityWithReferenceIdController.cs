using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactAdminRestServer.Definitions;
using System;

namespace ReactAdminRestServer.Controllers
{
    /// <summary>
    /// A base controller for class-based identifiers, such as strings.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TReadModel"></typeparam>
    /// <typeparam name="TCreateModel"></typeparam>
    /// <typeparam name="TUpdateModel"></typeparam>
    /// <typeparam name="TIndentifier">Must be class, such as string, or Guid.  See also the guid controller.</typeparam>
    [ApiController]
    [Produces("application/json")]
    public abstract class EntityWithReferenceIdController<TEntity,  TReadModel, TCreateModel, TUpdateModel, TIndentifier> : 
        EntityWithIdControllerBase<TEntity,  TReadModel, TCreateModel, TUpdateModel, TIndentifier>
        where TEntity : class, IHaveIdentifier<TIndentifier>
        where TIndentifier : class 
    {
        public EntityWithReferenceIdController(
            DbContext     dbContext, 
            IMapper       mapper,
            string        identityPropertyName,
            CacheInterval cacheInterval = CacheInterval.ThirtySeconds,
            int           pageSize      = 50
            ) : base(dbContext, mapper, identityPropertyName, cacheInterval, pageSize) { }
    }
}
