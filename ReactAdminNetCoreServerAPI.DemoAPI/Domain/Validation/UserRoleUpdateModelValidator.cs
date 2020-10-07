using System;
using FluentValidation;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Validation
{
    public partial class UserRoleUpdateModelValidator
        : AbstractValidator<UserRoleUpdateModel>
    {
        public UserRoleUpdateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
