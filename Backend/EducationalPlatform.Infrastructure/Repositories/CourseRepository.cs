using EducationalPlatform.Domain.Courses;
using Infrastructure.Presistence.Data;
using Infrastructure.Repositories.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Infrastructure.Repositories
{
    public class CourseRepository:GenericRepository<Course>,ICourseRepository
    {
        public CourseRepository(AppDbContext context) : base(context)
        {
        }
    }
}
