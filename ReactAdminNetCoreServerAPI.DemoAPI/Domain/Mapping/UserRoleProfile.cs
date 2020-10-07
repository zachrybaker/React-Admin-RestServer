using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class UserRoleProfile
        : AutoMapper.Profile
    {
        public UserRoleProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.UserRole, ReactAdminRestServer.DemoAPI.Domain.Models.UserRoleReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.UserRoleCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.UserRole>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.UserRole, ReactAdminRestServer.DemoAPI.Domain.Models.UserRoleUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.UserRoleUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.UserRole>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.UserRoleReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.UserRoleUpdateModel>();

        }

    }
}
