using System.Threading.Tasks;
using Course.IdentityServer.Models;
using Course.SharedLibrary.Dtos;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace Course.IdentityServer.Services.Concrete
{
    public class IdentityResourceOwnerPasswordValidator:IResourceOwnerPasswordValidator
    {
        private UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var existUser = await _userManager.FindByNameAsync(context.UserName);
            if (existUser == null)
            {
                context.Result.Error = "Password or Email is fail";
                return;
            }

            var successPassword =await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (successPassword != true)
            {
                context.Result.Error = "Password or Email is fail";
                return;
            }

            context.Result =
                new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
        }
    }
}
