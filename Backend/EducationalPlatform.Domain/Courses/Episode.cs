using EducationalPlatform.EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.EducationalPlatform.Domain.Courses;

public sealed class Episode : Entity<EpisodeId>
{
    public EpisodeName Name  { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public string VideoLink { get; private set; }
    public int Order { get; private set; }
    public CourseId CourseId { get; private set; }

    private Episode(EpisodeId id,CourseId courseId,EpisodeName name, string description,string imageUrl,string videoLink,int order): base(id)
    {
        CourseId = courseId;
        Name  = name;
        Description = description;
        ImageUrl = imageUrl;
        VideoLink = videoLink;
        Order = order;
    }

    private Episode() { }

    internal static Result<Episode> Create(
        CourseId courseId,
        string name,
        string description,
        string imageUrl,
        string videoLink,
        int    order)
    {
        if (courseId ==null)
            return Result<Episode>.Failure(CourseErrors.Episode.InvalidCourseId);

        var nameResult = EpisodeName.Create(name);
        if (nameResult.IsFailure)
            return Result<Episode>.Failure(nameResult.Error);

        var episode = new Episode(
            EpisodeId.New(), courseId, nameResult.Value,
            description, imageUrl, videoLink, order);

        return Result<Episode>.Success(episode);
    }
}
