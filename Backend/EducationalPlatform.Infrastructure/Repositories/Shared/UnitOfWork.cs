using Domain.Interfaces.Repositories;
using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Users;
using EducationalPlatform.Infrastructure.Repositories;
using EducationalPlatform.Infrastructure.Repositories.Shared;
using Infrastructure.Presistence.Data;

namespace Infrastructure.Repositories.Shared
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private ICourseRepository _courseRepository;
        private IUserRepository _userRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Lazy initialization of repositories
        public ICourseRepository Courses => _courseRepository ??= new CourseRepository(_context);
        public IUserRepository Users => _userRepository ??= new UserRepository(_context);

        // Commit changes
        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        // Dispose pattern
        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}