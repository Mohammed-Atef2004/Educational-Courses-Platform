using EducationalPlatform.Domain.SharedKernel;

namespace EducationalPlatform.Domain.Courses;


public static class CourseErrors
{
   
    public static readonly Error NotFound =
        new("Course.NotFound", "The requested course does not exist.");

    public static readonly Error AlreadyPublished =
        new("Course.AlreadyPublished", "The course has already been published.");

    public static readonly Error NotPublished =
        new("Course.NotPublished", "The course must be published before enrollment.");

    // Name Value Object 
    public static class Name
    {
        public static readonly Error Empty =
            new("Course.Name.Empty", "Course name cannot be empty.");

        public static readonly Error TooShort =
            new("Course.Name.TooShort",
                $"Course name must be at least {ValueObjects.CourseName.MinLength} characters.");

        public static readonly Error TooLong =
            new("Course.Name.TooLong",
                $"Course name must not exceed {ValueObjects.CourseName.MaxLength} characters.");
    }

    // Description Value Object
    public static class Description
    {
        public static readonly Error Empty =
            new("Course.Description.Empty", "Course description cannot be empty.");

        public static readonly Error TooShort =
            new("Course.Description.TooShort",
                $"Course description must be at least {ValueObjects.CourseDescription.MinLength} characters.");

        public static readonly Error TooLong =
            new("Course.Description.TooLong",
                $"Course description must not exceed {ValueObjects.CourseDescription.MaxLength} characters.");
    }

    // Money Value Object
    public static class Price
    {
        public static readonly Error Negative =
            new("Course.Price.Negative", "Course price cannot be negative.");

        public static readonly Error InvalidCurrency =
            new("Course.Price.InvalidCurrency", "Currency must be a valid 3-letter ISO code (e.g. EGP).");
    }

    //  Episode entity
    public static class Episode
    {
        public static readonly Error NameEmpty =
            new("Episode.Name.Empty", "Episode name cannot be empty.");

        public static readonly Error NameTooShort =
            new("Episode.Name.TooShort",
                $"Episode name must be at least {ValueObjects.EpisodeName.MinLength} characters.");

        public static readonly Error NameTooLong =
            new("Episode.Name.TooLong",
                $"Episode name must not exceed {ValueObjects.EpisodeName.MaxLength} characters.");

        public static readonly Error InvalidCourseId =
            new("Episode.InvalidCourseId", "Episode must belong to a valid course.");

        public static readonly Error DuplicateName =
            new("Episode.DuplicateName", "An episode with this name already exists in the course.");
    }

    // Business rules
    public static class Rules
    {
        public static readonly Error CourseNameMustBeUnique =
            new("Course.Rules.NameMustBeUnique", "A course with this name already exists.");
    }
}
