using Course.Services.Catalog.Dtos;
using Course.Services.Catalog.Dtos.PurchasedCoursesOfUser;
using Course.Services.Catalog.Models;
using Course.SharedLibrary.Dtos;

namespace Course.Services.Catalog.Services
{
    public interface IPurchasedCoursesOfUserService
    {
        Task<ResponseDto<List<PurchasedCoursesOfUserDto>>> GetAllAsync();
        Task<ResponseDto<PurchasedCoursesOfUserDto>> CreateAsync(CreatePurchasedCoursesOfUserDto createPurchasedCoursesOfUserDto);
        Task<ResponseDto<List<PurchasedCoursesOfUserDto>>> CreateRangeAsync(ICollection<CreatePurchasedCoursesOfUserDto> createPurchasedCoursesOfUserDtos);
        Task<ResponseDto<PurchasedCoursesOfUserDto>> GetByIdAsync(string id);
        Task<ResponseDto<bool>> DeleteAsync(string id);
        Task<ResponseDto<PurchasedCoursesOfUserDto>> UpdateAsync(CreatePurchasedCoursesOfUserDto createPurchasedCoursesOfUserDto);
        ResponseDto<List<PurchasedCoursesOfUserDto>> GetByUserId(string userId);
    }
}
