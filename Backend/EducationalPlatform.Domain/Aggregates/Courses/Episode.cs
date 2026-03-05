using EducationalPlatform.Domain.Aggregates.Courses.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Aggregates.Courses;

public sealed class Episode : Entity<Guid>
{
    public EpisodeName Name  { get; private set; }
    public string Description { get; private set; }
    public string ImageUrl { get; private set; }
    public string VideoLink { get; private set; }
    public int Order { get; private set; }
    public Guid CourseId { get; private set; }

    private Episode(Guid id,Guid courseId,EpisodeName name, string description,string imageUrl,string videoLink,int order): base(id)
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
        Guid   courseId,
        string name,
        string description,
        string imageUrl,
        string videoLink,
        int    order)
    {
        if (courseId == Guid.Empty)
            return Result<Episode>.Failure(CourseErrors.Episode.InvalidCourseId);

        var nameResult = EpisodeName.Create(name);
        if (nameResult.IsFailure)
            return Result<Episode>.Failure(nameResult.Error);

        var episode = new Episode(
            Guid.NewGuid(), courseId, nameResult.Value,
            description, imageUrl, videoLink, order);

        return Result<Episode>.Success(episode);
    }
}
