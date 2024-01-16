using System.Security.Claims;
using System.Threading.Tasks;
using Course.IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;

namespace Course.IdentityServer.CustomProfiles
{
    public class CustomProfileService:IProfileService
    {
        private UserManager<ApplicationUser> _userManager;

        public CustomProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user =await _userManager.GetUserAsync(context.Subject);
            if (user != null)
            {
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    context.IssuedClaims.Add(new Claim("email_verified", "true"));
                }
                context.IssuedClaims.Add(new Claim("username", user.Email));
                context.IssuedClaims.Add(new Claim("fullname", user.FullName));
                context.IssuedClaims.Add(new Claim("city", user.City));
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            if (user != null)
            {
                if (await _userManager.IsEmailConfirmedAsync(user))
                {
                    context.IsActive = true;
                }
                else
                {
                    context.IsActive = false;
                }
            }
            else
            {
                context.IsActive = false; // Kullanıcı bulunamadı, pasif
            }

        }
    }
}
