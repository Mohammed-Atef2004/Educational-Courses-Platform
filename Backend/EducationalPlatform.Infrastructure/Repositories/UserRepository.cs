using EducationalPlatform.Domain.Users;
using EducationalPlatform.Domain.Users.ValueObjects;
using Infrastructure.Presistence.Data;
using Infrastructure.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // ─── Queries ──────────────────────────────────────────────────────────────

        public Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => _context.Users
                .FirstOrDefaultAsync(u => u.Id == id, ct);

        public Task<User?> GetByEmailAsync(Email email, CancellationToken ct = default)
            => _context.Users
                .FirstOrDefaultAsync(u => u.Email.Value == email.Value, ct);

        public Task<User?> GetByUsernameAsync(Username username, CancellationToken ct = default)
            => _context.Users
                .FirstOrDefaultAsync(u => u.Username.Value == username.Value, ct);

        public Task<User?> GetByIdentityIdAsync(string identityId, CancellationToken ct = default)
            => _context.Users
                .FirstOrDefaultAsync(u => u.IdentityId == identityId, ct);

        public Task<bool> ExistsByEmailAsync(Email email, CancellationToken ct = default)
            => _context.Users
                .AnyAsync(u => u.Email.Value == email.Value, ct);

        public Task<bool> ExistsByUsernameAsync(Username username, CancellationToken ct = default)
            => _context.Users
                .AnyAsync(u => u.Username.Value == username.Value, ct);

        public async Task<IReadOnlyList<User>> GetByRoleAsync(UserRole role, CancellationToken ct = default)
            => await _context.Users
                .Where(u => u.Role == role)
                .ToListAsync(ct);

        // ─── Persistence ──────────────────────────────────────────────────────────

        public async Task AddAsync(User user, CancellationToken ct = default)
            => await _context.Users.AddAsync(user, ct);

        public void Update(User user)
            => _context.Users.Update(user);

        
        public void SoftDelete(User user)
            => _context.Users.Update(user);
    }
}
