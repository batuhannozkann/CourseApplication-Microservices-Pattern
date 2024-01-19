using Course.Services.Photostock.Dtos;
using Course.Services.Photostock.Services;
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
            FirebaseStorageService firebaseStorageService = new("courseapplication-f3e34.appspot.com");
            string url = await firebaseStorageService.UploadFileAsync(file);
            return CreateActionResultInstance(ResponseDto<string>.Success(url, 200));
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
