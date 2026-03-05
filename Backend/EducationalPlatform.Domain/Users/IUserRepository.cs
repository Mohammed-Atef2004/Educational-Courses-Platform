using EducationalPlatform.Domain.Users.ValueObjects;

namespace EducationalPlatform.Domain.Users;


public interface IUserRepository
{
    Task<User?>GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?>GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool>ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);
    void Add(User user);
    void Update(User user);
}
