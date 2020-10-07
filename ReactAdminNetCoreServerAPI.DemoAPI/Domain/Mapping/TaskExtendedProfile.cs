using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class TaskExtendedProfile
        : AutoMapper.Profile
    {
        public TaskExtendedProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.TaskExtended, ReactAdminRestServer.DemoAPI.Domain.Models.TaskExtendedReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.TaskExtendedCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.TaskExtended>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.TaskExtended, ReactAdminRestServer.DemoAPI.Domain.Models.TaskExtendedUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.TaskExtendedUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.TaskExtended>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.TaskExtendedReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.TaskExtendedUpdateModel>();

        }

    }
}
