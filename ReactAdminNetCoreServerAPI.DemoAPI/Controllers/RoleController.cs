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
    [Route("api/[controller]")]
    public class RoleController : EntityWithGuidIdController<Role, RoleReadModel, RoleCreateModel, RoleUpdateModel>
    {
        public RoleController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "Id", CacheInterval.OneDay)
        { }
    }
}
