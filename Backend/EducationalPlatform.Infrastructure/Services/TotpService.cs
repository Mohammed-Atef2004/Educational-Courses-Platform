using EducationalPlatform.Application.Features.Users.Services;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// Implementation of ITotpService using Otp.NET library.
    /// NuGet: Install-Package Otp.NET
    /// </summary>
    public sealed class TotpService : ITotpService
    {
        private const string AppName = "EducationalPlatform";

        public string GenerateSecret()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            return Base32Encoding.ToString(key);
        }

        public string GenerateQrCodeUri(string email, string secret)
        {
            var encodedEmail = Uri.EscapeDataString(email);
            var encodedApp = Uri.EscapeDataString(AppName);

            return $"otpauth://totp/{encodedApp}:{encodedEmail}" +
                   $"?secret={secret}" +
                   $"&issuer={encodedApp}" +
                   $"&algorithm=SHA1" +
                   $"&digits=6" +
                   $"&period=30";
        }

        public bool Verify(string secret, string code)
        {
            try
            {
                var key = Base32Encoding.ToBytes(secret);
                var totp = new OtpNet.Totp(key);

                return totp.VerifyTotp(
                    totp: code,
                    timeStepMatched: out _,
                    window: new VerificationWindow(previous: 1, future: 1));
            }
            catch
            {
                return false;
            }
        }
    }

}
