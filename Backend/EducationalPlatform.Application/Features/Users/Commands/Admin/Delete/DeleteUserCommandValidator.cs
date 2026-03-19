using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Admin.Delete
{
    public sealed class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(x => x.Reason)
                .NotEmpty().WithMessage("Deletion reason is required.")
                .MaximumLength(500).WithMessage("Reason must not exceed 500 characters.");
        }
    }
}
