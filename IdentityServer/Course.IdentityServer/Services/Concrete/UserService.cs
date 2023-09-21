using System.Collections.Generic;
using System.Linq;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Course.SharedLibrary.Dtos;
using System.Threading.Tasks;
using Course.IdentityServer.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace Course.IdentityServer.Services.Concrete
{
    public class UserService:IUserService
    {
        private UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<ResponseDto<ApplicationUser>> RegisterUser(UserRegisterDto userRegisterDto)
        {
            var user = new ApplicationUser()
            {
                City = userRegisterDto.City,
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.UserName
            };
            var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
            if (!result.Succeeded)
            {
                return ResponseDto<ApplicationUser>.Fail("Register process has failed",400);
            }

            var createdUser = await _userManager.FindByNameAsync(userRegisterDto.UserName);
            return ResponseDto<ApplicationUser>.Success(createdUser,200);
        }

        public ResponseDto<List<ApplicationUser>> GetAllUser()
        {
            List<ApplicationUser> users = _userManager.Users.ToList();
            return ResponseDto<List<ApplicationUser>>.Success(users,200);
        }

        
    }
}
