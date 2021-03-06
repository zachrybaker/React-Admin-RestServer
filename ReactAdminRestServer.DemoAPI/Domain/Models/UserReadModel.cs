using System;
using System.Collections.Generic;

namespace ReactAdminRestServer.DemoAPI.Domain.Models
{
    public partial class UserReadModel
    {
        #region Generated Properties
        public Guid Id { get; set; }

        public string EmailAddress { get; set; }

        public bool IsEmailAddressConfirmed { get; set; }

        public string DisplayName { get; set; }

        public string PasswordHash { get; set; }

        public string ResetHash { get; set; }

        public string InviteHash { get; set; }

        public int AccessFailedCount { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        public DateTimeOffset? LastLogin { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset Updated { get; set; }

        public string UpdatedBy { get; set; }

        #endregion

        /// <summary>
        /// Added this to show how we can reshape client-side data at the model level.
        /// In this case we will return the user's roles and their role IDs.
        /// </summary>
        public List<UserRoleReadModel> UserRoles { get; set; }
        
        
    }
}
