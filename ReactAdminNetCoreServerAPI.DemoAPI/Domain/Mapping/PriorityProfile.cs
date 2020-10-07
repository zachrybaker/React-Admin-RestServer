using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class PriorityProfile
        : AutoMapper.Profile
    {
        public PriorityProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Priority, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.PriorityReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.PriorityCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Priority>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Priority, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.PriorityUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.PriorityUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Priority>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.PriorityReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.PriorityUpdateModel>();

        }

    }
}
