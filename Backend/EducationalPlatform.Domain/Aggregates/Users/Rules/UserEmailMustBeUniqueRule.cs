using EducationalPlatform.Domain.SharedKernel;
using EducationalPlatform.Domain.Aggregates.Users;
using EducationalPlatform.Domain.Aggregates.Users.ValueObjects;

namespace EducationalPlatform.Domain.Aggregates.Users.Rules;

public sealed class UserEmailMustBeUniqueRule : IBusinessRule
{
    private readonly bool _isEmailTaken;

    public UserEmailMustBeUniqueRule(bool isEmailTaken)
        => _isEmailTaken = isEmailTaken;

    public bool IsBroken() => _isEmailTaken;

    public Error Error => UserErrors.Rules.EmailMustBeUnique;
}
