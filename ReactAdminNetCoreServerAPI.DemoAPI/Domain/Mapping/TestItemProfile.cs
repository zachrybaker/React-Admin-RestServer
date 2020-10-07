using System;
using AutoMapper;
using ReactAdminRestServer.DemoAPI.Data.Entities;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Mapping
{
    public partial class TestItemProfile
        : AutoMapper.Profile
    {
        public TestItemProfile()
        {
            CreateMap<ReactAdminRestServer.DemoAPI.Data.Entities.TestItem, ReactAdminRestServer.DemoAPI.Domain.Models.TestItemReadModel>();
        }

    }
}
