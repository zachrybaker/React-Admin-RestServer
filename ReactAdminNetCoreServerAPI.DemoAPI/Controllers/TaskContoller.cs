using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminNetCoreServerAPI.DemoAPI.Data;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.Common.Definitions;
using ReactAdminNetCoreServerAPI.Common.Controllers;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : EntityWithGuidIdController<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Task, TaskReadModel, TaskCreateModel, TaskUpdateModel>
    {
        public TaskController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "Id", CacheInterval.OneDay)
        { }
    }
}
