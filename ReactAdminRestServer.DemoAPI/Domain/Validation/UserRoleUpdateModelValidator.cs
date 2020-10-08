using System;
using FluentValidation;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Validation
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
