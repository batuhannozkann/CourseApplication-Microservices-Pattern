using Course.SharedLibrary.ControllerBases;
using Course.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        [HttpGet]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(ResponseDto<bool>.Success(true, 200));
        }
    }
}
