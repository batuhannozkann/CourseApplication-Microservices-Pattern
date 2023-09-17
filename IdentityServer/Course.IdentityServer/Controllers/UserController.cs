using System.Threading.Tasks;
using Course.IdentityServer.Dtos;
using Course.IdentityServer.Services.Abstract;
using Course.SharedLibrary.ControllerBases;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.IdentityServer.Controllers
{
    [Authorize(Policy = IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]/[Action]")]
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

        
    }
}
