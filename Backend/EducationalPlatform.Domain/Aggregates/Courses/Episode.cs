using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EducationalPlatform.Domain.Primitives;
using EducationalPlatform.Domain.Shared;

namespace EducationalPlatform.Domain.Aggregates.Courses
{
    public sealed class Episode : Entity
    {
       
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public string Link { get; private set; }

      
        public Guid CourseId { get; private set; }

        private Episode(Guid id, Guid courseId, string name, string description, string imageUrl, string link)
            : base(id)
        {
            CourseId = courseId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Link = link;
        }

    
        public static Result<Episode> Create(
            Guid courseId,
            string name,
            string description,
            string imageUrl,
            string link)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result<Episode>.Failure(CourseErrors.EpisodeEmptyName); 

            if (courseId == Guid.Empty)
                return Result<Episode>.Failure(CourseErrors.InvalidCourseId);

            var episode = new Episode(Guid.NewGuid(), courseId, name, description, imageUrl, link);

            return Result<Episode>.Success(episode);
        }
    }
}
