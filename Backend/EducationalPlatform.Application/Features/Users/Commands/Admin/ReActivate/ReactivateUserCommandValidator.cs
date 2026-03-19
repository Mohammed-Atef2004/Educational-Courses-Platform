using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.ReActivate
{
    public sealed class ReactivateUserCommandValidator : AbstractValidator<ReactivateUserCommand>
    {
        public ReactivateUserCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.ActorId)
                .NotEmpty().WithMessage("Actor ID is required.");
        }
    }

}
