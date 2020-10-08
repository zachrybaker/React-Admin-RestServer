using System;
using FluentValidation;
using ReactAdminRestServer.DemoAPI.Domain.Models;

namespace ReactAdminRestServer.DemoAPI.Domain.Validation
{
    public partial class AuditUpdateModelValidator
        : AbstractValidator<AuditUpdateModel>
    {
        public AuditUpdateModelValidator()
        {
            #region Generated Constructor
            RuleFor(p => p.Content).NotEmpty();
            RuleFor(p => p.Username).NotEmpty();
            RuleFor(p => p.Username).MaximumLength(50);
            RuleFor(p => p.CreatedBy).MaximumLength(100);
            RuleFor(p => p.UpdatedBy).MaximumLength(100);
            #endregion
        }

    }
}
