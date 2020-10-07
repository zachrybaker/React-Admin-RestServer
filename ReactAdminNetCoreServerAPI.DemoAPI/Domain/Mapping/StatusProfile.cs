using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class StatusProfile
        : AutoMapper.Profile
    {
        public StatusProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Status, ReactAdminRestServer.DemoAPI.Domain.Models.StatusReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.StatusCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Status>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Status, ReactAdminRestServer.DemoAPI.Domain.Models.StatusUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.StatusUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Status>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.StatusReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.StatusUpdateModel>();

        }

    }
}
