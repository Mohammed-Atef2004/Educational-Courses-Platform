using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace EducationalPlatform.Application.Features.Courses.Commands.CreateCourse
{
    public sealed class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.InstructorId)
                .NotEmpty().WithMessage("Instructor ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Course name cannot be empty.")
                .MinimumLength(3).WithMessage("Course name must be at least 3 characters.")
                .MaximumLength(150).WithMessage("Course name must not exceed 150 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Course description cannot be empty.")
                .MinimumLength(10).WithMessage("Course description must be at least 10 characters.")
                .MaximumLength(2000).WithMessage("Course description must not exceed 2000 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Course price cannot be negative.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be exactly a 3-letter ISO code (e.g., EGP).");

            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Course image URL is required.");

            RuleFor(x => x.VideoLink)
                .NotEmpty().WithMessage("Course promo video link is required.");
        }
    }
}
