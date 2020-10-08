using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReactAdminRestServer.DemoAPI.Data.Mapping
{
    public partial class TestItemMap
        : IEntityTypeConfiguration<ReactAdminRestServer.DemoAPI.Data.Entities.TestItem>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ReactAdminRestServer.DemoAPI.Data.Entities.TestItem> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("TestItem", "dbo");

            // key
            builder.HasKey(t => t.Key);

            // properties
            builder.Property(t => t.Key)
                .IsRequired()
                .HasColumnName("Key")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Value)
                .HasColumnName("Value")
                .HasColumnType("varchar(250)")
                .HasMaxLength(250);

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "TestItem";
        }

        public struct Columns
        {
            public const string Key = "Key";
            public const string Value = "Value";
        }
        #endregion
    }
}
