using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class UserRoleProfile
        : AutoMapper.Profile
    {
        public UserRoleProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.UserRole, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserRoleReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserRoleCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.UserRole>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.UserRole, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserRoleUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserRoleUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.UserRole>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserRoleReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserRoleUpdateModel>();

        }

    }
}
