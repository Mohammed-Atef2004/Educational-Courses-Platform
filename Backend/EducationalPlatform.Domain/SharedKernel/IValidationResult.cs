using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.EducationalPlatform.Domain.SharedKernel
{

    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError",
            "A validation error occurred.");

        Error[] Errors { get; }
    }
}
