using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Aggregates.Courses.Rules;

public sealed class CourseNameMustBeUniqueRule : IBusinessRule
{
    private readonly bool _isTaken;

    public CourseNameMustBeUniqueRule(bool isTaken) => _isTaken = isTaken;

    public bool IsBroken() => _isTaken;
    public Error Error     => CourseErrors.Rules.CourseNameMustBeUnique;
}


public sealed class EpisodeNameMustBeUniqueInCourseRule : IBusinessRule
{
    private readonly bool _isDuplicate;

    public EpisodeNameMustBeUniqueInCourseRule(bool isDuplicate) => _isDuplicate = isDuplicate;

    public bool IsBroken() => _isDuplicate;
    public Error Error     => CourseErrors.Episode.DuplicateName;
}
