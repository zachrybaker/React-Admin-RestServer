using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class StatusProfile
        : AutoMapper.Profile
    {
        public StatusProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Status, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.StatusReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.StatusCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Status>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Status, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.StatusUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.StatusUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Status>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.StatusReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.StatusUpdateModel>();

        }

    }
}
