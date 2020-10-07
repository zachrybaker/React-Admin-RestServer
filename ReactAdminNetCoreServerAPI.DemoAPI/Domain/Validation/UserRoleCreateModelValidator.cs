using System;
using FluentValidation;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Validation
{
    public partial class UserRoleCreateModelValidator
        : AbstractValidator<UserRoleCreateModel>
    {
        public UserRoleCreateModelValidator()
        {
            #region Generated Constructor
            #endregion
        }

    }
}
