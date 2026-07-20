using EducationalPlatform.Domain.SharedKernel;

namespace  EducationalPlatform.Domain.Users.Events;


public sealed record UserRegisteredDomainEvent(
    Guid UserId,
    string Email,
    string FullName,
    string Username,
    UserRole Role) : DomainEvent
{
    private Guid id;
    private string value;
    private string displayName;

    public UserRegisteredDomainEvent(Guid id, string value, string displayName)
    {
        this.id = id;
        this.value = value;
        this.displayName = displayName;
    }
}

