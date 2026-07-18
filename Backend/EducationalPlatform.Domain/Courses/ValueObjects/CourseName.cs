using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Courses.ValueObjects;

public sealed class CourseName : ValueObject
{
    public const int MinLength = 3;
    public const int MaxLength = 150;

    public string Value { get; }

    private CourseName(string value) => Value = value;

    public static Result<CourseName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<CourseName>.Failure(CourseErrors.Name.Empty);

        var trimmed = value.Trim();

        if (trimmed.Length < MinLength)
            return Result<CourseName>.Failure(CourseErrors.Name.TooShort);

        if (trimmed.Length > MaxLength)
            return Result<CourseName>.Failure(CourseErrors.Name.TooLong);

        return Result<CourseName>.Success(new CourseName(trimmed));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;
}
