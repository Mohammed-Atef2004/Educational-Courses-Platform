using Domain.Users;
using EducationalPlatform.Domain.Interfaces.Repositories;
using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Infrastructure.Repositories;
using Infrastructure.Presistence.Data;

namespace Infrastructure.Repositories.Shared
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
     

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }



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