using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Dtos.Common;
using Course.Services.Catalog.Dtos.PurchasedCoursesOfUser;
using Course.Services.Catalog.Services;
using Course.SharedLibrary.ControllerBases;
using Course.SharedLibrary.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection.Internal;
using Microsoft.AspNetCore.Mvc;

namespace Course.Services.Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : CustomBaseController
{
   private readonly ICourseService _courseService;
    private readonly ISharedIdentityService _sharedIdentityService;
    private readonly IPurchasedCoursesOfUserService _purchasedCoursesOfUserService;

    public CourseController(ICourseService courseService, ISharedIdentityService sharedIdentityService, IPurchasedCoursesOfUserService purchasedCoursesOfUserService)
    {
        _courseService = courseService;
        _sharedIdentityService = sharedIdentityService;
        _purchasedCoursesOfUserService = purchasedCoursesOfUserService;
    }

    [HttpGet]
    [AllowAnonymous]
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
      return CreateActionResultInstance(await _courseService.GetByIdAsync(id,_sharedIdentityService.GetUserId));
   }
    [HttpPost("[action]")]
    public async Task<IActionResult> CreatePurchasedCourseToUser(CreatePurchasedCoursesOfUserDto createPurchasedCoursesOfUserDto)
    {
        if(createPurchasedCoursesOfUserDto.UserId==null)
        {
            createPurchasedCoursesOfUserDto.UserId = _sharedIdentityService.GetUserId;
        }
        return CreateActionResultInstance(await _purchasedCoursesOfUserService.CreateAsync(createPurchasedCoursesOfUserDto));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> CreatePurchasedCoursesToUser([FromBody]ICollection<CreatePurchasedCoursesOfUserDto> createPurchasedCoursesOfUserDtos)
    {
        foreach(var dto in createPurchasedCoursesOfUserDtos)
        {
            if (dto.UserId == null)
            {
                dto.UserId = _sharedIdentityService.GetUserId;
            }
        }
        return CreateActionResultInstance(await _purchasedCoursesOfUserService.CreateRangeAsync(createPurchasedCoursesOfUserDtos));
    }
    [HttpGet("[action]")]
   public async Task<IActionResult> GetByUserIdAsync(string? userId)
   {
      if(userId==null)
        {
            userId = _sharedIdentityService.GetUserId;
        }
      return CreateActionResultInstance(await _courseService.GetByUserId(userId));
   }

   [HttpGet("[action]")]
   public async Task<IActionResult> GetByPurchasedCourseByLoginUser()
    {
        return CreateActionResultInstance(_purchasedCoursesOfUserService.GetByUserId(_sharedIdentityService.GetUserId));
    }
    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllCourseAccordingToUser()
    {
        return CreateActionResultInstance(await _courseService.GetAllCourseAccordingToUser(_sharedIdentityService.GetUserId));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> GetFilteredCourses(FilterParameters filterParameters)
    {
        return CreateActionResultInstance(await _courseService.GetFilteredCourses(filterParameters));
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