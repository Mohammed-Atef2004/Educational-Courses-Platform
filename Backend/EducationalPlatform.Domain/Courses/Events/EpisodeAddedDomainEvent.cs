using EducationalPlatform.EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EducationalPlatform.EducationalPlatform.Domain.Courses.CourseErrors;

namespace EducationalPlatform.EducationalPlatform.Domain.Courses.Events
{

    public sealed record EpisodeAddedDomainEvent(
        CourseId CourseId,
        EpisodeId EpisodeId,
        string EpisodeName) : DomainEvent;

}
