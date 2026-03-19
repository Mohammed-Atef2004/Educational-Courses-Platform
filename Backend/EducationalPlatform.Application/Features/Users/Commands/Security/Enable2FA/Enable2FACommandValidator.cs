using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Security.Enable2FA
{
    public sealed class Enable2FACommandValidator : AbstractValidator<Enable2FACommand>
    {
        public Enable2FACommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.TotpCode)
                .NotEmpty().WithMessage("TOTP code is required.")
                .Length(6).WithMessage("TOTP code must be 6 digits.")
                .Matches(@"^\d{6}$").WithMessage("TOTP code must contain digits only.");
        }
    }
}
