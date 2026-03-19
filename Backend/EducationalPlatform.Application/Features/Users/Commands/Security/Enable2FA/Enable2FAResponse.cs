using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users.Commands.Security.Enable2FA
{
    public sealed record Enable2FAResponse(string Secret, string QrCodeUri);

}
