using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.Deactivate
{
    public sealed class DeactivateUserCommandValidator : AbstractValidator<DeactivateUserCommand>
    {
        public DeactivateUserCommandValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Deactivation reason is required.")
                .MaximumLength(500).WithMessage("Reason must not exceed 500 characters.");
        }
    }
}
