using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class AuditProfile
        : AutoMapper.Profile
    {
        public AuditProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Audit, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.AuditReadModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.AuditCreateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Audit>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Audit, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.AuditUpdateModel>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.AuditUpdateModel, ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Audit>();

            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.AuditReadModel, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.AuditUpdateModel>();

        }

    }
}
