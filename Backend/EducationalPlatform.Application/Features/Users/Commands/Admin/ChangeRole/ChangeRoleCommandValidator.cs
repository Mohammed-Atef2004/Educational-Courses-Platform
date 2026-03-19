using EducationalPlatform.Domain.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.ChangeRole
{
    public sealed class ChangeRoleCommandValidator : AbstractValidator<ChangeRoleCommand>
    {
        public ChangeRoleCommandValidator()
        {
            RuleFor(x => x.TargetUserId)
                .NotEmpty().WithMessage("Target user ID is required.");

            RuleFor(x => x.ActorId)
                .NotEmpty().WithMessage("Actor ID is required.");

            RuleFor(x => x.NewRole)
                .IsInEnum().WithMessage("Invalid role.")
                .NotEqual(UserRole.SuperAdmin)
                .WithMessage("SuperAdmin role cannot be assigned via this endpoint.");
        }
    }
}
