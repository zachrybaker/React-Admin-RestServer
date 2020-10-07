using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Data
{
    public partial class DemoAPIContext : DbContext
    {
        public DemoAPIContext(DbContextOptions<DemoAPIContext> options)
            : base(options)
        {
        }

        #region Generated Properties
        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Audit> Audits { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Priority> Priorities { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Role> Roles { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Status> Statuses { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.TaskExtended> TaskExtendeds { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.Task> Tasks { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.TestItem> TestItems { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.UserRole> UserRoles { get; set; }

        public virtual DbSet<ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities.User> Users { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Generated Configuration
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.AuditMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.PriorityMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.RoleMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.StatusMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.TaskExtendedMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.TaskMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.TestItemMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.UserMap());
            modelBuilder.ApplyConfiguration(new ReactAdminNetCoreServerAPI.DemoAPI.Data.Mapping.UserRoleMap());
            #endregion
        }
    }
}
