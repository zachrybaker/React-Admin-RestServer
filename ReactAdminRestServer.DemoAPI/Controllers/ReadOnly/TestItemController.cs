using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminRestServer.DemoAPI.Data;
using ReactAdminRestServer.DemoAPI.Domain.Models;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.Controllers;
using System;
using System.Linq.Expressions;

namespace ReactAdminRestServer.DemoAPI.Controllers
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
