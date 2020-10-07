using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactAdminRestServer.DemoAPI.Data;
using ReactAdminRestServer.DemoAPI.Domain.Models;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.Common.Definitions;
using ReactAdminRestServer.Common.Controllers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReactAdminRestServer.DemoAPI.Controllers
{
    /// <summary>
    /// This controller demonstrates:
    /// - joining and filtering.
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : EntityWithGuidIdController<User, UserReadModel, UserCreateModel, UserUpdateModel>
    {
        public UserController(DemoAPIContext dataContext, IMapper mapper) : base(dataContext, mapper, "Id", CacheInterval.OneDay)
        { }
        
        // Include the Roles on the detail response.
        protected override IQueryable<User> IncludeTheseForDetailResponse(IQueryable<User> queryable) => 
            queryable
                .Include(x => x.UserRoles)         // get the user's role IDs...
                .ThenInclude(y => y.Role)          // and then those corresponding roles.
                .Where(x => x.IsDeleted == false); // This where could have been saved for the WithFilters override...
    }
}
