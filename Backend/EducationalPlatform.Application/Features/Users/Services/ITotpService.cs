using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Services
{
    public interface ITotpService
    {
       
        string GenerateSecret();

        string GenerateQrCodeUri(string email, string secret);

        bool Verify(string secret, string code);
    }
}
