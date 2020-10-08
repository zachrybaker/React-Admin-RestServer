using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class AuditProfile
        : AutoMapper.Profile
    {
        public AuditProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Audit, ReactAdminRestServer.DemoAPI.Domain.Models.AuditReadModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.AuditCreateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Audit>();

            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.Audit, ReactAdminRestServer.DemoAPI.Domain.Models.AuditUpdateModel>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.AuditUpdateModel, ReactAdminRestServer.DemoAPI.Data.Entities.Audit>();

            CreateMap<ReactAdminRestServer.DemoAPI.Domain.Models.AuditReadModel, ReactAdminRestServer.DemoAPI.Domain.Models.AuditUpdateModel>();

        }

    }
}
