using EducationalPlatform.Domain.Courses.Enums;

namespace EducationalPlatform.Api.Endpoints.Courses.GetCourse
{
    public sealed record GetCourseResponse
   (
      Guid InstructorId,
      string Name,
      string Description,
      decimal Price,
      string ImageUrl,
      string VideoLink,
      CourseStatus CourseStatus);
}
