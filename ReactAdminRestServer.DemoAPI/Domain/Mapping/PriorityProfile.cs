using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class PriorityProfile
        : AutoMapper.Profile
    {
        public PriorityProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Priority, ReactAdminRestServer.DemoAPI.Domain.Models.PriorityReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.PriorityCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Priority>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Priority, ReactAdminRestServer.DemoAPI.Domain.Models.PriorityUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.PriorityUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Priority>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.PriorityReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.PriorityUpdateModel>();

        }

    }
}
