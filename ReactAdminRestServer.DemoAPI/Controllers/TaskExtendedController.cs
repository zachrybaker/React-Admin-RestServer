using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminRestServer.DemoAPI.Data;
using ReactAdminRestServer.DemoAPI.Domain.Models;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.Definitions;
using ReactAdminRestServer.Controllers;
using System;
using System.Linq.Expressions;

namespace ReactAdminRestServer.DemoAPI.Controllers
{
    /// <summary>
    /// This controller demonstrates:
    /// - using the EntityWithIdControllerBase although it could have used EntityWithGuidIdController
    /// - handling a non "Id" identifier
    /// </summary>
    [Route("api/[controller]")]
    public class TaskExtendedController : EntityWithIdControllerBase<TaskExtended, TaskExtendedReadModel, TaskExtendedCreateModel, TaskExtendedUpdateModel, Guid>
    {
        public TaskExtendedController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "TaskId", CacheInterval.OneDay)
        { }

        #region Expressions
        /// <summary>
        /// If you need to use a primay key other than "Id", then override this.
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        protected override Expression<Func<TaskExtended, bool>> GetIdentifierEqualityFn(Guid g)  => (x => x.TaskId == g);
        #endregion Expressions
    }
}
