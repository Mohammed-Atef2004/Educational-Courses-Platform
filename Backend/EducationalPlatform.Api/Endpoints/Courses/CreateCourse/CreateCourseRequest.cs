namespace EducationalPlatform.Api.Endpoints.Courses.CreateCourse
{
    public sealed record CreateCourseRequest(
      string Name,
      string Description,
      decimal Price,
      string Currency,
      string ImageUrl,
      string VideoLink
  );
}
