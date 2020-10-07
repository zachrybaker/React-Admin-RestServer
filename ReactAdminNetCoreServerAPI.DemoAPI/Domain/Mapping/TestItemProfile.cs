using System;
using AutoMapper;
using ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Mapping
{
    public partial class TestItemProfile
        : AutoMapper.Profile
    {
        public TestItemProfile()
        {
            CreateMap<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.TestItem, ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models.TestItemReadModel>();
        }

    }
}
