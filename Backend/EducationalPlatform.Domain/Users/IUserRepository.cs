using EducationalPlatform.Domain.Users.ValueObjects;

namespace EducationalPlatform.Domain.Users;


public interface IUserRepository
{

   
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default);
    Task<bool> ExistsByUsernameAsync(Username username, CancellationToken ct = default);

    Task<IReadOnlyList<User>> GetByRoleAsync(UserRole role, CancellationToken ct = default);

    // ─── Persistence ─────────────────────────────────────────────────────────

    void SoftDelete(User user);   // sets IsDeleted — EF change tracker handles the rest
}  

