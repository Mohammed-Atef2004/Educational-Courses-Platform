using System;
using System.Collections.Generic;
using System.Text;

namespace EducationalPlatform.Domain.Shared
{
    public sealed record Error(string Code, string Description)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");
    }
}
