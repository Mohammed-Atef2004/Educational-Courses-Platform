using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Courses.ValueObjects;

public sealed class CourseDescription : ValueObject
{
    public const int MinLength = 20;
    public const int MaxLength = 2000;

    public string Value { get; }

    private CourseDescription(string value) => Value = value;

    public static Result<CourseDescription> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<CourseDescription>.Failure(CourseErrors.Description.Empty);

        var trimmed = value.Trim();

        if (trimmed.Length < MinLength)
            return Result<CourseDescription>.Failure(CourseErrors.Description.TooShort);

        if (trimmed.Length > MaxLength)
            return Result<CourseDescription>.Failure(CourseErrors.Description.TooLong);

        return Result<CourseDescription>.Success(new CourseDescription(trimmed));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
