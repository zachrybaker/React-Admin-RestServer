using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminRestServer.DemoAPI.Data;
using ReactAdminRestServer.DemoAPI.Domain.Models;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.Definitions;
using ReactAdminRestServer.Controllers;

namespace ReactAdminRestServer.DemoAPI.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : EntityWithGuidIdController<ReactAdminRestServer.DemoAPI.Data.Entities.Task, TaskReadModel, TaskCreateModel, TaskUpdateModel>
    {
        public TaskController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "Id", CacheInterval.OneDay)
        { }
    }
}
