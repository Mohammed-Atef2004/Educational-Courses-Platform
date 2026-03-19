using EducationalPlatform.EducationalPlatform.Domain.Courses.Events;
using EducationalPlatform.EducationalPlatform.Domain.Courses.Rules;
using EducationalPlatform.EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.EducationalPlatform.Domain.Courses;

public sealed class Course : AggregateRoot<CourseId>
{
   

    public CourseName Name { get; private set; }
    public CourseDescription Description { get; private set; }
    public Money Price { get; private set; }

    public string ImageUrl  { get; private set; }
    public string VideoLink { get; private set; }

    public bool IsPublished  { get; private set; }
    public DateTime? PublishedAt  { get; private set; }

    public IReadOnlyCollection<Episode> Episodes => _episodes.AsReadOnly();
    private readonly List<Episode> _episodes = new();

    

    private Course(CourseId id,CourseName name,CourseDescription description,Money price,string imageUrl,string videoLink): base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
        VideoLink = videoLink;
        IsPublished = false;
    }

  
    private Course() { }

    public static Result<Course> Create(
        string name,
        string description,
        decimal price,
        string currency,
        string imageUrl,
        string videoLink,
        bool isNameTaken)
    {
        //  Name 
        var nameResult = CourseName.Create(name);
        if (nameResult.IsFailure)
            return Result<Course>.Failure(nameResult.Error);

        //  Description 
        var descResult = CourseDescription.Create(description);
        if (descResult.IsFailure)
            return Result<Course>.Failure(descResult.Error);

        //  Money 
        var priceResult = Money.Create(price, currency);
        if (priceResult.IsFailure)
            return Result<Course>.Failure(priceResult.Error);

        //  Business Rule
        var course = new Course(
            CourseId.New(),
            nameResult.Value,
            descResult.Value,
            priceResult.Value,
            imageUrl,
            videoLink);

        var ruleResult = course.CheckRule(new CourseNameMustBeUniqueRule(isNameTaken));
        if (ruleResult.IsFailure)
            return Result<Course>.Failure(ruleResult.Error);

        //  Domain event
        course.AddDomainEvent(new CourseCreatedDomainEvent(
            course.Id,
            course.Name.Value,
            course.Price.Amount,
            course.Price.Currency));

        return Result<Course>.Success(course);
    }

  
    public Result AddEpisode(
        string name,
        string description,
        string imageUrl,
        string videoLink)
    {
        // duplicate episode name within this course
        var isDuplicate = _episodes.Any(e =>
            string.Equals(e.Name.Value, name?.Trim(), StringComparison.OrdinalIgnoreCase));

        var ruleResult = CheckRule(new EpisodeNameMustBeUniqueInCourseRule(isDuplicate));
        if (ruleResult.IsFailure)
            return Result.Failure(ruleResult.Error);

        var order  = _episodes.Count + 1;
        var episodeResult = Episode.Create(Id, name, description, imageUrl, videoLink, order);
        if (episodeResult.IsFailure)
            return Result.Failure(episodeResult.Error);

        _episodes.Add(episodeResult.Value);

        AddDomainEvent(new EpisodeAddedDomainEvent(
            Id, episodeResult.Value.Id, episodeResult.Value.Name.Value));

        return Result.Success();
    }

    public Result UpdatePrice(decimal newAmount, string currency)
    {
        var priceResult = Money.Create(newAmount, currency);
        if (priceResult.IsFailure)
            return Result.Failure(priceResult.Error);

        var oldPrice = Price;
        Price        = priceResult.Value;

        AddDomainEvent(new CoursePriceUpdatedDomainEvent(
            Id, oldPrice.Amount, Price.Amount, Price.Currency));

        return Result.Success();
    }

    public Result Publish()
    {
        if (IsPublished)
            return Result.Failure(CourseErrors.AlreadyPublished);

        if (!_episodes.Any())
            return Result.Failure(new Error(
                "Course.Publish.NoEpisodes",
                "Cannot publish a course with no episodes."));

        IsPublished = true;
        PublishedAt = DateTime.UtcNow;

        AddDomainEvent(new CoursePublishedDomainEvent(Id, Name.Value));
        return Result.Success();
    }
}
