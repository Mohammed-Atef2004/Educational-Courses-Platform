using EducationalPlatform.Domain.Users;
using EducationalPlatform.Domain.Courses;

namespace EducationalPlatform.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {

        Task<int> CompleteAsync(CancellationToken cancellationToken = default);
    }
}