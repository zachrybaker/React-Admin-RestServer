using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class TaskProfile
        : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Task, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Task>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Task, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Task>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TaskUpdateModel>();

        }

    }
}
