using  EducationalPlatform.Domain.Users.Errors;
using  EducationalPlatform.Domain.Users.Events;
using  EducationalPlatform.Domain.Users.Rules;
using  EducationalPlatform.Domain.Users.ValueObjects;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using static  EducationalPlatform.Domain.Users.Errors.UserErrors;

namespace  EducationalPlatform.Domain.Users;


public sealed class User : AggregateRoot<Guid>
{
    /// <summary>Reference to the Identity Provider record (e.g. ASP.NET Identity).</summary>
    public string IdentityId { get; private set; } = default!;

    public Email Email { get; private set; } = default!;
    public Username Username { get; private set; } = default!;
    public FullName FullName { get; private set; } = default!;
    public PhoneNumber? PhoneNumber { get; private set; }   // optional

    // ─── Authorization ─────────────────────────────────────────────────────────
    public UserRole Role { get; private set; }

    // ─── Lifecycle ─────────────────────────────────────────────────────────────
    public bool IsActive { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTime RegisteredAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeleteReason { get; private set; }


    public DateTime? LastLoginAt { get; private set; }

    public int FailedLoginAttempts { get; private set; }

    public DateTime? LockedUntil { get; private set; }

    // ─── 2FA ───────────────────────────────────────────────────────────────────

    public bool IsTwoFactorEnabled { get; private set; }

    /// <summary>
    /// Base-32 TOTP secret.  Stored encrypted at-rest by the Infrastructure layer.
    /// Exposed here as a plain string so the domain stays persistence-ignorant.
    /// </summary>
    public string? TwoFactorSecret { get; private set; }

    // ─── Password History ──────────────────────────────────────────────────────

    /// <summary>
    /// Stores the last N password hashes (BCrypt).
    /// The Application layer is responsible for hashing; the domain records history.
    /// </summary>
    private readonly List<string> _passwordHashes = new();
    public IReadOnlyList<string> PasswordHashes => _passwordHashes.AsReadOnly();

    /// <summary>UTC timestamp of the most recent password change.</summary>
    public DateTime? PasswordChangedAt { get; private set; }

    // ─── Constructor / Factory ─────────────────────────────────────────────────

    private User() { }

    private User(
        Guid id,
        string identityId,
        Email email,
        Username username,
        FullName fullName,
        UserRole role) : base(id)
    {
        IdentityId = identityId;
        Email = email;
        Username = username;
        FullName = fullName;
        Role = role;
        IsActive = true;
        IsDeleted = false;
        RegisteredAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Creates a new user, enforcing email + username uniqueness.
    /// </summary>
    public static Result<User> Create(
        string identityId,
        string email,
        string username,
        string firstName,
        string lastName,
        bool isEmailTaken,
        bool isUsernameTaken,
        UserRole role = UserRole.Student)
    {
        // ── Validate identity reference ──
        if (string.IsNullOrWhiteSpace(identityId))
            return Result<User>.Failure(UserErrors.InvalidIdentityReference);

        // ── Build Value Objects ──
        var emailResult = Email.Create(email);
        if (emailResult.IsFailure)
            return Result<User>.Failure(emailResult.Error);

        var usernameResult = Username.Create(username);
        if (usernameResult.IsFailure)
            return Result<User>.Failure(usernameResult.Error);

        var fullNameResult = FullName.Create(firstName, lastName);
        if (fullNameResult.IsFailure)
            return Result<User>.Failure(fullNameResult.Error);

        // ── Enforce business rules ──
        var user = new User(
            Guid.NewGuid(),
            identityId,
            emailResult.Value,
            usernameResult.Value,
            fullNameResult.Value,
            role);

        var emailUnique = user.CheckRule(new UserEmailMustBeUniqueRule(isEmailTaken));
        if (emailUnique.IsFailure)
            return Result<User>.Failure(emailUnique.Error);

        var usernameUnique = user.CheckRule(new UsernameMustBeUniqueRule(isUsernameTaken));
        if (usernameUnique.IsFailure)
            return Result<User>.Failure(usernameUnique.Error);

        // ── Raise domain event ──
        user.AddDomainEvent(new UserRegisteredDomainEvent(
            user.Id,
            user.Email.Value,
            user.FullName.DisplayName,
            user.Username.Value,
            user.Role));

        return Result<User>.Success(user);
    }

    // ─── Profile Operations ────────────────────────────────────────────────────

    public Result ChangeName(string firstName, string lastName)
    {
        if (!CanModify()) return Result.Failure(UserErrors.Deactivated);

        var result = FullName.Create(firstName, lastName);
        if (result.IsFailure) return Result.Failure(result.Error);

        var oldName = FullName.DisplayName;
        FullName = result.Value;

        AddDomainEvent(new UserNameChangedDomainEvent(Id, oldName, FullName.DisplayName));
        return Result.Success();
    }

    public Result SetPhoneNumber(string phoneNumber)
    {
        if (!CanModify()) return Result.Failure(UserErrors.Deactivated);

        var result = PhoneNumber.Create(phoneNumber);
        if (result.IsFailure) return Result.Failure(result.Error);

        var oldPhone = PhoneNumber?.Value;
        PhoneNumber = result.Value;

        AddDomainEvent(new UserPhoneNumberChangedDomainEvent(Id, oldPhone, PhoneNumber.Value));
        return Result.Success();
    }

    public Result RemovePhoneNumber()
    {
        if (!CanModify()) return Result.Failure(UserErrors.Deactivated);

        var oldPhone = PhoneNumber?.Value;
        PhoneNumber = null;

        AddDomainEvent(new UserPhoneNumberChangedDomainEvent(Id, oldPhone, null));
        return Result.Success();
    }

    // ─── Role Operations ───────────────────────────────────────────────────────

    public Result ChangeRole(UserRole newRole)
    {
        if (!CanModify()) return Result.Failure(UserErrors.Deactivated);
        if (Role == newRole) return Result.Failure(UserErrors.RoleAlreadyAssigned);

        // SuperAdmin is only seeded, never assigned through normal flow
        if (newRole == UserRole.SuperAdmin)
            return Result.Failure(UserErrors.CannotAssignSuperAdminRole);

        var oldRole = Role;
        Role = newRole;

        AddDomainEvent(new UserRoleChangedDomainEvent(Id, oldRole, newRole));
        return Result.Success();
    }

    /// <summary>Convenience shortcut used by the platform.</summary>
    public Result PromoteToInstructor() => ChangeRole(UserRole.Instructor);

    // ─── Lifecycle Operations ─────────────────────────────────────────────────

    public Result Deactivate(string reason)
    {
        if (Role == UserRole.SuperAdmin)
            return Result.Failure(UserErrors.CannotDeactivateSuperAdmin);

        if (!IsActive)
            return Result.Failure(UserErrors.Deactivated);

        IsActive = false;

        AddDomainEvent(new UserDeactivatedDomainEvent(Id, reason));
        return Result.Success();
    }

    public Result Reactivate()
    {
        if (IsDeleted) return Result.Failure(UserErrors.AlreadyDeleted);
        if (IsActive) return Result.Failure(UserErrors.AlreadyActive);

        IsActive = true;

        AddDomainEvent(new UserReactivatedDomainEvent(Id));
        return Result.Success();
    }

    /// <summary>Soft delete — data is retained, user cannot log in.</summary>
    public Result Delete(string reason)
    {
        if (Role == UserRole.SuperAdmin)
            return Result.Failure(UserErrors.CannotDeleteSuperAdmin);

        if (IsDeleted)
            return Result.Failure(UserErrors.AlreadyDeleted);

        IsDeleted = true;
        IsActive = false;
        DeletedAt = DateTime.UtcNow;
        DeleteReason = reason;

        AddDomainEvent(new UserDeletedDomainEvent(Id, Email.Value, reason));
        return Result.Success();
    }

    // ─── Security Operations ──────────────────────────────────────────────────

    /// <summary>
    /// Records a successful login.  Resets lockout counters.
    /// </summary>
    public Result RecordSuccessfulLogin(string ipAddress)
    {
        if (IsLockedOut())
            return Result.Failure(SecurityErrors.AccountLocked);

        if (!CanModify())
            return Result.Failure(UserErrors.Deactivated);

        FailedLoginAttempts = 0;
        LockedUntil = null;
        LastLoginAt = DateTime.UtcNow;

        AddDomainEvent(new UserLoggedInSuccessfullyDomainEvent(Id, ipAddress, LastLoginAt.Value));
        return Result.Success();
    }

    /// <summary>
    /// Records a failed login attempt and locks the account when the threshold is exceeded.
    /// </summary>
    public Result RecordFailedLogin(string ipAddress)
    {
        if (IsDeleted)
            return Result.Failure(UserErrors.AlreadyDeleted);

        FailedLoginAttempts++;

        AddDomainEvent(new UserLoginAttemptFailedDomainEvent(Id, FailedLoginAttempts, ipAddress));

        var lockoutRule = CheckRule(new AccountLockoutRule(FailedLoginAttempts));
        if (lockoutRule.IsFailure)
        {
            // Lock for 15 minutes
            LockedUntil = DateTime.UtcNow.AddMinutes(15);
            AddDomainEvent(new UserAccountLockedDomainEvent(Id, LockedUntil.Value));
        }

        return Result.Success();
    }

    /// <summary>Manually unlocks an account (Admin action).</summary>
    public Result UnlockAccount()
    {
        FailedLoginAttempts = 0;
        LockedUntil = null;

        AddDomainEvent(new UserAccountUnlockedDomainEvent(Id));
        return Result.Success();
    }

    // ─── 2FA ─────────────────────────────────────────────────────────────────

    /// <summary>
    /// Enables TOTP-based 2FA.  The secret must be generated + verified
    /// by the Application layer before calling this method.
    /// </summary>
    public Result EnableTwoFactor(string secret)
    {
        if (IsTwoFactorEnabled)
            return Result.Failure(SecurityErrors.TwoFactorAlreadyEnabled);

        if (string.IsNullOrWhiteSpace(secret))
            return Result.Failure(SecurityErrors.InvalidTwoFactorSecret);

        TwoFactorSecret = secret;
        IsTwoFactorEnabled = true;

        AddDomainEvent(new UserTwoFactorEnabledDomainEvent(Id));
        return Result.Success();
    }

    public Result DisableTwoFactor()
    {
        if (!IsTwoFactorEnabled)
            return Result.Failure(SecurityErrors.TwoFactorNotEnabled);

        TwoFactorSecret = null;
        IsTwoFactorEnabled = false;

        AddDomainEvent(new UserTwoFactorDisabledDomainEvent(Id));
        return Result.Success();
    }

    // ─── Password History ─────────────────────────────────────────────────────

    /// <summary>
    /// Records a new password hash.
    /// The Application layer must check reuse BEFORE calling this
    /// (using <see cref="PasswordHashes"/> + a hash-compare service),
    /// then pass the <paramref name="isPasswordReused"/> result here.
    /// </summary>
    public Result RecordPasswordChange(string newPasswordHash, bool isPasswordReused)
    {
        if (!CanModify()) return Result.Failure(UserErrors.Deactivated);

        var reuseRule = CheckRule(new PasswordMustNotBeReusedRule(isPasswordReused));
        if (reuseRule.IsFailure) return Result.Failure(reuseRule.Error);

        _passwordHashes.Add(newPasswordHash);

        // Keep only the last N hashes (trim older ones)
        const int maxHistory = PasswordMustNotBeReusedRule.HistoryDepth;
        while (_passwordHashes.Count > maxHistory)
            _passwordHashes.RemoveAt(0);

        PasswordChangedAt = DateTime.UtcNow;

        AddDomainEvent(new UserPasswordChangedDomainEvent(Id, PasswordChangedAt.Value));
        return Result.Success();
    }

    // ─── Private Helpers ──────────────────────────────────────────────────────

    /// <summary>True when the account is unlocked and not soft-deleted.</summary>
    private bool CanModify() => IsActive && !IsDeleted && !IsLockedOut();

    /// <summary>True when a lockout is currently in effect.</summary>
    private bool IsLockedOut() =>
        LockedUntil.HasValue && LockedUntil.Value > DateTime.UtcNow;
}