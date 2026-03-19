using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationalPlatform.Application.Features.Users
{
    public static class AuditActions
    {
        public const string UserRegistered = "User.Registered";
        public const string UserLogin = "User.Login";
        public const string UserLoginFailed = "User.LoginFailed";
        public const string UserLogout = "User.Logout";
        public const string UserRevokeAllSessions = "User.RevokeAllSessions";
        public const string UserRefreshToken = "User.RefreshToken";
        public const string UserConfirmEmail = "User.ConfirmEmail";
        public const string UserForgotPassword = "User.ForgotPassword";
        public const string UserResetPassword = "User.ResetPassword";
        public const string UserChangePassword = "User.ChangePassword";
        public const string UserChangeEmail = "User.ChangeEmail";
        public const string UserChangeName = "User.ChangeName";
        public const string UserSetPhoneNumber = "User.SetPhoneNumber";
        public const string UserRemovePhoneNumber = "User.RemovePhoneNumber";
        public const string UserChangeRole = "User.ChangeRole";
        public const string UserDeactivated = "User.Deactivated";
        public const string UserReactivated = "User.Reactivated";
        public const string UserDeleted = "User.Deleted";
        public const string UserUnlocked = "User.Unlocked";
        public const string User2FAEnabled = "User.2FAEnabled";
        public const string User2FADisabled = "User.2FADisabled";
    }
}
