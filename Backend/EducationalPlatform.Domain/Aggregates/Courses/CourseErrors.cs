using EducationalPlatform.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Aggregates.Courses
{
    public static class CourseErrors
    {
        public static readonly Error EmptyName = 
            new( "Course.EmptyName", "The course name cannot be empty."); 

        public static readonly Error NegativePrice =
            new("Course.NegativePrice", "The course price cannot be negative."); 

        public static readonly Error DescriptionTooShort =
            new("Course.DescriptionTooShort", "The course description is too short.");

        public static readonly Error EpisodeEmptyName = 
            new("Episode.EmptyName", "The episode name cannot be empty.");

        public static readonly Error InvalidCourseId = 
            new("Episode.InvalidCourseId", "The course ID is invalid.");
    }
}
