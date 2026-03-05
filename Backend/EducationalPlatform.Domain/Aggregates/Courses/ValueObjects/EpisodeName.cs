using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Aggregates.Courses.ValueObjects;

public sealed class EpisodeName : ValueObject
{
    public const int MinLength = 3;
    public const int MaxLength = 200;

    public string Value { get; }

    private EpisodeName(string value) => Value = value;

    public static Result<EpisodeName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result<EpisodeName>.Failure(CourseErrors.Episode.NameEmpty);

        var trimmed = value.Trim();

        if (trimmed.Length < MinLength)
            return Result<EpisodeName>.Failure(CourseErrors.Episode.NameTooShort);

        if (trimmed.Length > MaxLength)
            return Result<EpisodeName>.Failure(CourseErrors.Episode.NameTooLong);

        return Result<EpisodeName>.Success(new EpisodeName(trimmed));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value.ToLowerInvariant();
    }

    public override string ToString() => Value;
}
