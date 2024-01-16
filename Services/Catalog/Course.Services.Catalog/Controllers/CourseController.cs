using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Services;
using Course.SharedLibrary.ControllerBases;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : CustomBaseController
{
   private readonly ICourseService _courseService;

   public CourseController(ICourseService courseService)
   {
      _courseService = courseService;
   }
   [HttpGet]
   public async Task<IActionResult> GetAllAsync()
   {
      return CreateActionResultInstance(await _courseService.GetAllAsync());
   }
   [HttpPost]
   public async Task<IActionResult> CreateAsync(CourseCreateDto courseCreateDto)
   {
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