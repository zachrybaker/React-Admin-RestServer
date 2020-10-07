using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class UserProfile
        : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.User, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.User>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.User, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.User>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.UserUpdateModel>();

        }

    }
}
