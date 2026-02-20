using EducationalPlatform.Domain.Aggregates.Courses.Events;
using EducationalPlatform.Domain.Aggregates.Courses.ValueObjects;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Aggregates.Courses
{
    public sealed class Course : AggregateRoot
    {
        public CourseName Name { get; private set; }
        public CourseDescription Description { get; private set; }
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; }
        public string Link { get; private set; }


        public IReadOnlyCollection<Episode> Episodes => _episodes.AsReadOnly();
        private readonly List<Episode> _episodes = new();

        private Course(Guid id, CourseName name, CourseDescription description, decimal price, string imageUrl, string link)
            : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            Link = link;
        }

        public static Result<Course> Create(string name, string description, decimal price, string imageUrl, string link)
        {
            var nameResult = CourseName.Create(name);
            if (nameResult.IsFailure)
            {
                return Result<Course>.Failure(nameResult.Error);
            }
            var descriptionResult = CourseDescription.Create(description);
            if (descriptionResult.IsFailure) return Result<Course>.Failure(descriptionResult.Error);

            if (price < 0)
                return Result<Course>.Failure(CourseErrors.NegativePrice);

            var course = new Course(Guid.NewGuid(), nameResult.Value, descriptionResult.Value, price, imageUrl, link);
            course.RaiseDomainEvent(new CourseCreatedDomainEvent(course.Id));

            return Result<Course>.Success(course); 
        }

        public Result AddEpisode(string name, string description, string imageUrl, string link)
        {
            var episodeResult = Episode.Create(Id, name, description, imageUrl, link);
            if (episodeResult.IsFailure)
            {
                return Result.Failure(episodeResult.Error); 
            }

            _episodes.Add(episodeResult.Value);
            return Result.Success(); 
        }
        public Result UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                return Result.Failure(CourseErrors.NegativePrice); 

            Price = newPrice;
            return Result.Success(); 
        }
    }
}
