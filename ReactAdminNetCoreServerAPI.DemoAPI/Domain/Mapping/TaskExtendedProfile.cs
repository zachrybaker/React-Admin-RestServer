using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class TaskExtendedProfile
        : AutoMapper.Profile
    {
        public TaskExtendedProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.TaskExtended, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskExtendedReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskExtendedCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.TaskExtended>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.TaskExtended, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskExtendedUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskExtendedUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.TaskExtended>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskExtendedReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskExtendedUpdateModel>();

        }

    }
}
