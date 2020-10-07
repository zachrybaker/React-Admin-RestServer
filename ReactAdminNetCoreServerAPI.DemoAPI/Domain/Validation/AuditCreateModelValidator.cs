using System;
using FluentValidation;
using ReactAdminNetCoreServerAPI.DemoAPI.Domain.Models;

namespace ReactAdminNetCoreServerAPI.DemoAPI.Domain.Validation
{
    public partial class AuditCreateModelValidator
        : AbstractValidator<AuditCreateModel>
    {
        public AuditCreateModelValidator()
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
