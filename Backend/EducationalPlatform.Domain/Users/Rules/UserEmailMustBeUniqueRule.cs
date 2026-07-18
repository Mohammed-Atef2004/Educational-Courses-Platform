using  EducationalPlatform.Domain.Users.ValueObjects;
using  EducationalPlatform.Domain.Users.Errors;
using EducationalPlatform.Domain.SharedKernel;

namespace  EducationalPlatform.Domain.Users.Rules;

public sealed class UserEmailMustBeUniqueRule : IBusinessRule
{
    private readonly bool _isEmailTaken;

    public UserEmailMustBeUniqueRule(bool isEmailTaken)
        => _isEmailTaken = isEmailTaken;

    public bool IsBroken() => _isEmailTaken;
    public Error Error => RulesErrors.EmailMustBeUnique;
}
