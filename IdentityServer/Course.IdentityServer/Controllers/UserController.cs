using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Models;
using Course.IdentityServer.Services.Abstract;
using Course.SharedLibrary.ControllerBases;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Course.IdentityServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private IUserService _userService;
        

        public UserController(IUserService userService)
        {
            _userService = userService;
            
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return CreateActionResultInstance(_userService.GetAllUser());
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            
            return CreateActionResultInstance(await _userService.RegisterUser(userRegisterDto));

        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string email)
        {
            
            return CreateActionResultInstance(await _userService.GetUser(email));

        }

        [HttpGet]
        public async Task<IActionResult> GenerateToken([FromQuery]string email)
        {
            return CreateActionResultInstance(await _userService.GenerateEmailConfirmationTokenAsync(email));
        }

        [HttpGet]
        public async Task<IActionResult> EmailConfirmationByToken([FromQuery]EmailConfirmationDto emailConfirmationDto)
        {
            return CreateActionResultInstance(await _userService.EmailConfirmationByTokenAsync(emailConfirmationDto.Email, emailConfirmationDto.Token));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateResetPasswordToken([FromQuery]string email)
        {
            return CreateActionResultInstance(await _userService.GeneratePasswordResetTokenAsync(email));
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            return CreateActionResultInstance(await _userService.ResetPasswordAsync(resetPasswordDto.Token,
                resetPasswordDto.Email, resetPasswordDto.Password));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserInfoDto userInfoDto)
        {
            return CreateActionResultInstance(await _userService.UpdateUserInfo(userInfoDto));
        }

    }
}
