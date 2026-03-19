using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.EducationalPlatform.Domain.SharedKernel
{

    public interface IBusinessRule
    {
        bool IsBroken();
        Error Error { get; }
    }


}
