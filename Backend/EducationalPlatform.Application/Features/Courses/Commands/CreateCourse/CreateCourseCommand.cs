using EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Courses.Commands.CreateCourse
{
    public sealed record CreateCourseCommand(
     Guid InstructorId,
     string Name,
     string Description,
     decimal Price,
     string Currency,
     string ImageUrl,
     string VideoLink
 ) : IRequest<Result<Guid>>;
}
