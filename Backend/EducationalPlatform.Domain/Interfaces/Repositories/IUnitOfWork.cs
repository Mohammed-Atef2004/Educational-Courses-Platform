using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.Users;

namespace Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ICourseRepository Courses { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}