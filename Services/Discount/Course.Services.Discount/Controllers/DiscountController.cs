using Course.Services.Discount.Services;
using Course.SharedLibrary.ControllerBases;
using Course.SharedLibrary.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Discount.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CustomBaseController
    {
        private readonly IDiscountService _discountService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return CreateActionResultInstance(await _discountService.GetById(id));
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> GetByUserIdAndCode(string code)
        {
            string userId = _sharedIdentityService.GetUserId;
            return CreateActionResultInstance(await _discountService.GetDiscountByUserIdAndCode(userId, code));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Add(discount));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.Delete(id));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Models.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }

    }
}
