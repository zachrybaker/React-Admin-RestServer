using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class RoleProfile
        : AutoMapper.Profile
    {
        public RoleProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Role, ReactAdminRestServer.DemoAPI.Domain.Models.RoleReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.RoleCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Role>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Role, ReactAdminRestServer.DemoAPI.Domain.Models.RoleUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.RoleUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Role>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.RoleReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.RoleUpdateModel>();

        }

    }
}
