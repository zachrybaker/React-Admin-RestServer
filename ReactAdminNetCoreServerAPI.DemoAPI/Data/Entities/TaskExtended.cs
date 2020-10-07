using ReactAdminRestServer.Common.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactAdminRestServer.DemoAPI.Data.Entities
{
    public partial class TaskExtended : IHaveIdentifier<Guid>
    {
        public TaskExtended()
        {
            #region Generated Constructor
            #endregion
        }
        
        [NotMapped]
        public Guid Id { get { return TaskId; } set { TaskId = value;} }

        #region Generated Properties
        public Guid TaskId { get; set; }

        public string UserAgent { get; set; }

        public string Browser { get; set; }

        public string OperatingSystem { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        public Byte[] RowVersion { get; set; }

        #endregion

        #region Generated Relationships
        public virtual Task Task { get; set; }

        #endregion

    }
}
