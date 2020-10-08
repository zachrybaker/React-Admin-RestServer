using System;
using System.Collections.Generic;

namespace ReactAdminRestServer.DemoAPI.Domain.Models
{
    public partial class UserRoleCreateModel
    {
        #region Generated Properties
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }

        #endregion
        
        /// <summary>
        /// Added this to show how we can reshape client-side data at the model level.
        /// In this case we will return the user's roles and their role IDs.
        /// </summary>
        public List<RoleReadModel> Roles { get; set; }
    }
}
