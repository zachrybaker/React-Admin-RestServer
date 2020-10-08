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
    [Route("api/[controller]")]
    public class RoleController : EntityWithGuidIdController<Role, RoleReadModel, RoleCreateModel, RoleUpdateModel>
    {
        public RoleController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "Id", CacheInterval.OneDay)
        { }
    }
}
