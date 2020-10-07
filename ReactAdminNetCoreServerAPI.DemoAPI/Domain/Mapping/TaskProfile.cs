using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class TaskProfile
        : AutoMapper.Profile
    {
        public TaskProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Task, ReactAdminRestServer.DemoAPI.Domain.Models.TaskReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.TaskCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Task>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Task, ReactAdminRestServer.DemoAPI.Domain.Models.TaskUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.TaskUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Task>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.TaskReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.TaskUpdateModel>();

        }

    }
}
