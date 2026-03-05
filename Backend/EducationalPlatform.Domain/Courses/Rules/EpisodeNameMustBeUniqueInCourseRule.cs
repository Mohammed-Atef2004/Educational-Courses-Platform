using EducationalPlatform.Domain.Courses;
using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Courses.Rules;


public sealed class EpisodeNameMustBeUniqueInCourseRule : IBusinessRule
{
    private readonly bool _isDuplicate;

    public EpisodeNameMustBeUniqueInCourseRule(bool isDuplicate) => _isDuplicate = isDuplicate;

    public bool IsBroken() => _isDuplicate;
    public Error Error     => CourseErrors.Episode.DuplicateName;
}
