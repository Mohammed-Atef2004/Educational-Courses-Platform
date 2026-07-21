using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Courses.ValueObjects;
using Infrastructure.Presistence.Data;
using Infrastructure.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class CourseRepository:ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Course course, CancellationToken cancellationToken = default)
        {
             await _context.Set<Course>().AddAsync(course, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(CourseName name, CancellationToken cancellationToken = default)
        {
            return await _context.Courses
                .AnyAsync(c => c.Name == name, cancellationToken);
        }

        public async Task<Course?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken = default)
        {
            
            var id = CourseId.From(courseId);

            return await _context.Courses.
                Include(e => e.Episodes)   // future work: consider using specifications pattern to include related entities only when needed ＼(ﾟｰﾟ＼)
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public void Update(Course course)
        {
            _context.Set<Course>().Update(course);
        }
    }
}
