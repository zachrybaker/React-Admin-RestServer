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
    public class PriorityController : 
        EntityWithGuidIdController<Priority, PriorityReadModel, PriorityCreateModel, PriorityUpdateModel>
    {
        public PriorityController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "Id", CacheInterval.OneDay)
        { }
    }
}
