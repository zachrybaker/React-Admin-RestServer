using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class RoleProfile
        : AutoMapper.Profile
    {
        public RoleProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Role, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.RoleReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.RoleCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Role>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Role, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.RoleUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.RoleUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Role>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.RoleReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.RoleUpdateModel>();

        }

    }
}
