using EducationalPlatform.Domain.Courses.ValueObjects;

namespace EducationalPlatform.Domain.Courses;

public interface ICourseRepository
{
    Task<Course?>GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool>ExistsByNameAsync(CourseName name, CancellationToken cancellationToken = default);
    void Add(Course course);
    void Update(Course course);
    void Remove(Course course);
}
