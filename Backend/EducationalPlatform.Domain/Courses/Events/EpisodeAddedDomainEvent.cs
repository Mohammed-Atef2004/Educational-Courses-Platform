using EducationalPlatform.Domain.Courses.ValueObjects;
using EducationalPlatform.Domain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EducationalPlatform.Domain.Courses.CourseErrors;

namespace EducationalPlatform.Domain.Courses.Events
{

    public sealed record EpisodeAddedDomainEvent(
        CourseId CourseId,
        EpisodeId EpisodeId,
        string EpisodeName) : DomainEvent;

}
