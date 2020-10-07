using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminNetCoreServerAPI.DemoAPI.Data;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.Common.Definitions;
using ReactAdminNetCoreServerAPI.Common.Controllers;
using System;
using System.Linq.Expressions;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Controllers
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
