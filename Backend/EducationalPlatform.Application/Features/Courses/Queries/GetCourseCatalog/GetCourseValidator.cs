using EducationalPlatform.Application.Features.Courses.Commands.CreateCourse;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Courses.Queries.GetCourseCatalog
{
    public class GetCourseValidator : AbstractValidator<GetCourseQuery>
    {
        public GetCourseValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("Course ID is required.");
        }
    }
}
