using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Models;
using Course.SharedLibrary.Dtos;

namespace Course.Services.Catalog.Services;

public interface ICategoryService
{
    Task<ResponseDto<List<CategoryDto>>> GetAllAsync();
    Task<ResponseDto<CategoryDto>> CreateAsync(CategoryCreateDto category);
    Task<ResponseDto<CategoryDto>> GetByIdAsync(string id);
    Task<ResponseDto<bool>> DeleteAsync(string id);
    Task<ResponseDto<CategoryDto>> UpdateAsync(CategoryDto categoryDto);

}