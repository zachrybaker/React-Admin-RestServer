using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class UserProfile
        : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.User, ReactAdminRestServer.DemoAPI.Domain.Models.UserReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.UserCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.User>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.User, ReactAdminRestServer.DemoAPI.Domain.Models.UserUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.UserUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.User>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.UserReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.UserUpdateModel>();

        }

    }
}
