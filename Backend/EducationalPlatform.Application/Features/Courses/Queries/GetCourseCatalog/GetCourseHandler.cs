using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Courses.Queries.GetCourseCatalog
{
    public class GetCourseHandler:IRequestHandler<GetCourseQuery, Result<GetCourseResponce>>
    {
        private readonly ICourseRepository _courseRepository;
        public GetCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Result<GetCourseResponce>> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetByIdAsync(request.Id, cancellationToken);
            if (course == null)
            {
                return Result<GetCourseResponce>.Failure(
           new Error("Course.NotFound", "Course not found."));
            }
            return Result<GetCourseResponce>.Success(new GetCourseResponce(
              course.InstructorId,
              course.Name.Value,
              course.Description.Value,
              course.Price.Amount,
               course.ImageUrl,
               course.VideoLink,
              course.CourseStatus
            ));
        }
    }
}
