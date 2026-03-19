using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Security.Disable2FA
{
    public sealed class Disable2FACommandValidator : AbstractValidator<Disable2FACommand>
    {
        public Disable2FACommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.");
        }
    }

}
