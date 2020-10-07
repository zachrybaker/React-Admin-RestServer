using ReactAdminNetCoreServerAPI.Common.Definitions;
using System;
using System.Collections.Generic;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Data.Entities
{
    public partial class Audit : IHaveIdentifier<Guid>
    {
        public Audit()
        {
            #region Generated Constructor
            #endregion
        }

        #region Generated Properties
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid? UserId { get; set; }

        public Guid? TaskId { get; set; }

        public string Content { get; set; }

        public string Username { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        #endregion

        #region Generated Relationships
        #endregion

    }
}
