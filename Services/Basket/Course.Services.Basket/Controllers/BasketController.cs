using Course.Services.Basket.Dtos;
using Course.Services.Basket.Services;
using Course.Services.Basket.Services.Abstract;
using Course.SharedLibrary.ControllerBases;
using Course.SharedLibrary.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var claims = User.Claims;
            return CreateActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }
        [HttpPost]
        public async Task<IActionResult> Save(BasketDto basketDto)
        {
            return CreateActionResultInstance(await _basketService.Save(basketDto));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            return CreateActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteElement([FromQuery]string courseId)
        {
            return CreateActionResultInstance(
                await _basketService.DeleteElementOnBasket(_sharedIdentityService.GetUserId, courseId));
        }

    }
}
