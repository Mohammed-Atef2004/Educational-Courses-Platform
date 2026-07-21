using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Courses.Enums;
using EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Courses.Queries.GetCourseCatalog
{
    public record GetCourseQuery
    (Guid Id) : IRequest<Result<GetCourseResponce>>;
    public record GetCourseResponce(
      Guid InstructorId,
      string Name,
      string Description,
      decimal Price,
      string ImageUrl,
      string VideoLink,
      CourseStatus CourseStatus
  );

}
