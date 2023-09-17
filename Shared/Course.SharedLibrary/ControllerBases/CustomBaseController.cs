using Course.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Course.SharedLibrary.ControllerBases
{
    public class CustomBaseController:ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(ResponseDto<T> responseDto)
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = responseDto.StatusCode
            };
        }
    }
}