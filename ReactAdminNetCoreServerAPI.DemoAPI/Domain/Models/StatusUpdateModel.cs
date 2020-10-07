using System;
using System.Collections.Generic;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models
{
    public partial class StatusUpdateModel
    {
        #region Generated Properties
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        #endregion

    }
}
