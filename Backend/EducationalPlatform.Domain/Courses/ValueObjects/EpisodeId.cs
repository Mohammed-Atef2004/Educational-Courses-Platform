using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Domain.Courses.ValueObjects
{
    public sealed record EpisodeId(Guid Id)
    {
        public static EpisodeId New() => new(Guid.NewGuid());
        public static EpisodeId From(Guid id) => new(id);
        public static EpisodeId From(string id) => new(Guid.Parse(id));
        public override string ToString() => Id.ToString();
    }
}
