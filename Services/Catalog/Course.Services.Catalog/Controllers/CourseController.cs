using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Services;
using Course.SharedLibrary.ControllerBases;
using Course.SharedLibrary.Services.Abstract;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : CustomBaseController
{
   private readonly ICourseService _courseService;
    private readonly ISharedIdentityService _sharedIdentityService;

    public CourseController(ICourseService courseService, ISharedIdentityService sharedIdentityService)
    {
        _courseService = courseService;
        _sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
   public async Task<IActionResult> GetAllAsync()
   {
      return CreateActionResultInstance(await _courseService.GetAllAsync());
   }
   [HttpPost]
   public async Task<IActionResult> CreateAsync([FromBody]CourseCreateDto courseCreateDto)
   {
        if (String.IsNullOrEmpty(courseCreateDto.UserId))
        {
            courseCreateDto.UserId = _sharedIdentityService.GetUserId;
        }
      return CreateActionResultInstance(await _courseService.CreateAsync(courseCreateDto));
   }
   [HttpGet("[action]")]
   public async Task<IActionResult> GetByIdAsync(string id)
   {
      return CreateActionResultInstance(await _courseService.GetByIdAsync(id));
   }
   [HttpGet("[action]")]
   public async Task<IActionResult> GetByUserIdAsync(string userId)
   {
      return CreateActionResultInstance(await _courseService.GetByUserId(userId));
   }

   [HttpPut]
   public async Task<IActionResult> UpdateAsync(CourseUpdateDto updateDto)
   {
      return CreateActionResultInstance(await _courseService.UpdateAsync(updateDto));
   }

   [HttpDelete]
   public async Task<IActionResult> RemoveAsync(string id)
   {
      return CreateActionResultInstance(await _courseService.RemoveAsync(id));
   }
}