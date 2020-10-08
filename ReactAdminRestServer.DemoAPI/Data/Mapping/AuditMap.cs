using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReactAdminRestServer.DemoAPI.Data.Mapping
{
    public partial class AuditMap
        : IEntityTypeConfiguration<ReactAdminRestServer.DemoAPI.Data.Entities.Audit>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ReactAdminRestServer.DemoAPI.Data.Entities.Audit> builder)
        {
            #region Generated Configure
            // table
            builder.ToTable("Audit", "dbo");

            // key
            builder.HasKey(t => t.Id);

            // properties
            builder.Property(t => t.Id)
                .IsRequired()
                .HasColumnName("Id")
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("(newsequentialid())");

            builder.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("datetime");

            builder.Property(t => t.UserId)
                .HasColumnName("UserId")
                .HasColumnType("uniqueidentifier");

            builder.Property(t => t.TaskId)
                .HasColumnName("TaskId")
                .HasColumnType("uniqueidentifier");

            builder.Property(t => t.Content)
                .IsRequired()
                .HasColumnName("Content")
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.Username)
                .IsRequired()
                .HasColumnName("Username")
                .HasColumnType("nvarchar(50)")
                .HasMaxLength(50);

            builder.Property(t => t.Created)
                .IsRequired()
                .HasColumnName("Created")
                .HasColumnType("datetimeoffset")
                .HasDefaultValueSql("(sysutcdatetime())");

            builder.Property(t => t.CreatedBy)
                .HasColumnName("CreatedBy")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.Updated)
                .IsRequired()
                .HasColumnName("Updated")
                .HasColumnType("datetimeoffset")
                .HasDefaultValueSql("(sysutcdatetime())");

            builder.Property(t => t.UpdatedBy)
                .HasColumnName("UpdatedBy")
                .HasColumnType("nvarchar(100)")
                .HasMaxLength(100);

            builder.Property(t => t.RowVersion)
                .IsRequired()
                .IsRowVersion()
                .HasColumnName("RowVersion")
                .HasColumnType("rowversion")
                .HasMaxLength(8)
                .ValueGeneratedOnAddOrUpdate();

            // relationships
            #endregion
        }

        #region Generated Constants
        public struct Table
        {
            public const string Schema = "dbo";
            public const string Name = "Audit";
        }

        public struct Columns
        {
            public const string Id = "Id";
            public const string Date = "Date";
            public const string UserId = "UserId";
            public const string TaskId = "TaskId";
            public const string Content = "Content";
            public const string Username = "Username";
            public const string Created = "Created";
            public const string CreatedBy = "CreatedBy";
            public const string Updated = "Updated";
            public const string UpdatedBy = "UpdatedBy";
            public const string RowVersion = "RowVersion";
        }
        #endregion
    }
}
