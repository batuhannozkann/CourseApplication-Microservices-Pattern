using Course.SharedLibrary.Dtos;

namespace Course.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<ResponseDto<List<Models.Discount>>> GetAll();
        Task<ResponseDto<Models.Discount>> GetById(int id);
        Task<ResponseDto<Models.Discount>> GetDiscountByUserIdAndCode(string userId, string code);
        Task<ResponseDto<bool>> Update(Models.Discount model);
        Task<ResponseDto<bool>> Delete(int id);
        Task<ResponseDto<bool>> Add(Models.Discount model);
    }
}
