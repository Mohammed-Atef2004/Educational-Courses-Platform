using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Aggregates.Courses
{
    public interface ICourseRepository
    {
    
        Task<Course?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(Course course);

        void Update(Course course);

        void Remove(Course course);
    }
}
