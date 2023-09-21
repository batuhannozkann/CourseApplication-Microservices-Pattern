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
    [Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private IUserService _userService;
        private UserManager<ApplicationUser> _userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
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
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim == null) return BadRequest();
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null) return BadRequest();
            return Ok(new ApplicationUser() { UserName = user.UserName, Email = user.Email, City = user.City });

        }

        
    }
}
