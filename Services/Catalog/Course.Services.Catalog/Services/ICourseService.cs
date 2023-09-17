using Course.Services.Catalog.Dtos;
using Course.SharedLibrary.Dtos;

namespace Course.Services.Catalog.Services;

public interface ICourseService
{
    Task<ResponseDto<List<CourseDto>>> GetAllAsync();
    Task<ResponseDto<CourseDto>> CreateAsync(CourseCreateDto createDto);
    Task<ResponseDto<CourseDto>> GetByIdAsync(string id);
    Task<ResponseDto<CourseDto>> RemoveAsync(string id);
    Task<ResponseDto<CourseDto>> UpdateAsync(CourseUpdateDto updateDto);
    Task<ResponseDto<List<CourseDto>>> GetByUserId(string userId);
}