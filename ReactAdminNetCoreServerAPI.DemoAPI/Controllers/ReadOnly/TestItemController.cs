using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminNetCoreServerAPI.DemoAPI.Data;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.Common.Controllers;
using System;
using System.Linq.Expressions;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Controllers
{
    /// <summary>
    /// This controller demonstrates:
    /// - use of a different "Id" identifier
    /// </summary>
    [Route("api/[controller]")]
    public class TestItemController : 
        ReadOnlyEntityWithIdControllerBase<
            TestItem, TestItemReadModel, string>
    {
        public TestItemController(DemoAPIContext dataContext, IMapper mapper) : 
            base(dataContext, mapper, "Key") { }
        
        protected override Expression<Func<TestItem, bool>> GetIdentifierEqualityFn(string o) 
            => (x => x.Key == o);
    }
}
