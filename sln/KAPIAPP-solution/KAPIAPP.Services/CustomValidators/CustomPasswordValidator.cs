using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KAPIAPP.Services.CustomValidators
{
    public class CustomPasswordValidator<TUser> : IPasswordValidator<TUser> where TUser : class
    {
        public async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user, string password)
        {
            var username = await manager.GetUserNameAsync(user);
            if (username.ToLower().Equals(password.ToLower()))
            {
                return IdentityResult.Failed(new IdentityError { Description = "le nom d'utilisateur est contenu dans le mot de passe", Code = "SameUserPass" });
            }

            if (password.ToLower().Contains("password"))
            {
                return IdentityResult.Failed(new IdentityError { Description = "le mot password est contenu dans le mot de passe", Code = "PasswordContainsPassword" });
            }

            return IdentityResult.Success;
        }
    }
}
