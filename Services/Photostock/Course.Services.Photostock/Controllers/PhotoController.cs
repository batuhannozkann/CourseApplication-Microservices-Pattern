using Course.Services.Photostock.Dtos;
using Course.SharedLibrary.ControllerBases;
using Course.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Photostock.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile file, CancellationToken cancellationToken)
        {
            if (file != null && file.Length != 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos", file.FileName);
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream, cancellationToken);
                }

                var returnPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", file.FileName);
                return CreateActionResultInstance(ResponseDto<PhotoDto>.Success(new PhotoDto() { Url = returnPath },
                    200));

            }
            return CreateActionResultInstance(ResponseDto<PhotoDto>.Fail( "Photo is empty",
                400));
        }

        [HttpDelete]
        public async Task<IActionResult> PhotoDelete(string fileUrl)
        {
            if (Path.Exists(fileUrl))
            {
                System.IO.File.Delete(fileUrl);
                return CreateActionResultInstance(ResponseDto<string>.Success("Photo deleted", 204));
            }
            return CreateActionResultInstance(ResponseDto<string>.Fail("Photo has not found", 404));
            
            
        }
    }
}
