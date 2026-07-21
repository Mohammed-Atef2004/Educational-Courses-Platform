using EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.Domain.Interfaces.Repositories;

namespace EducationalPlatform.Domain.Courses;

public interface ICourseRepository 
{
   
    

     Task AddAsync(Course course, CancellationToken cancellationToken = default);

     void Update(Course course); 

     Task<Course?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken = default);

     Task<bool> ExistsByNameAsync(CourseName name, CancellationToken cancellationToken = default);

}
