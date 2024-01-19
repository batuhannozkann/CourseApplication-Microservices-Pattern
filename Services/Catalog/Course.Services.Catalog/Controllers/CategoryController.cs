using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.Services.Catalog.Services;
using Course.Services.Catalog.Settings;
using Course.SharedLibrary.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Course.Services.Catalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : CustomBaseController
{
    private ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return CreateActionResultInstance(await _categoryService.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoryCreateDto category)
    {
        return CreateActionResultInstance(await _categoryService.CreateAsync(category));
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        return CreateActionResultInstance(await _categoryService.GetByIdAsync(id));
    }
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        return CreateActionResultInstance(await _categoryService.DeleteAsync(id));
    }
}