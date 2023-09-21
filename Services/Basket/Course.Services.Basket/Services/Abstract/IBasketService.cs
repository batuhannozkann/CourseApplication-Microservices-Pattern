using Course.Services.Basket.Dtos;
using Course.SharedLibrary.Dtos;

namespace Course.Services.Basket.Services.Abstract
{
    public interface IBasketService
    {
        Task<ResponseDto<BasketDto>> GetBasket(string userId);
        Task<ResponseDto<bool>> Save(BasketDto basket);
        Task<ResponseDto<bool>> Delete(string userId);
        Task<ResponseDto<bool>> DeleteElementOnBasket(string userId, string courseId);
    }
}
