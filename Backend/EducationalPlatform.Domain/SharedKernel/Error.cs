using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.EducationalPlatform.Domain.SharedKernel;

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);
}