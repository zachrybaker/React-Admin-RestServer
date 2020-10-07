using System;
using FluentValidation;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Validation
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
