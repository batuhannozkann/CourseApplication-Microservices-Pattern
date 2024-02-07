using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;
using Course.Services.Basket.Dtos;
using Course.Services.Basket.Services.Abstract;
using Course.SharedLibrary.Dtos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Course.Services.Basket.Services
{
    public class BasketService:IBasketService
    {
        private readonly RedisService _redisService;
        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<ResponseDto<BasketDto>> GetBasket(string userId)
        {
            var gotBasket = await _redisService.GetDatabase().StringGetAsync(userId);
            return String.IsNullOrEmpty(gotBasket)
                ? ResponseDto<BasketDto>.Fail("Basket not found", 404)
                : ResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(gotBasket), 200);
        }

        public async Task<ResponseDto<bool>> Save(BasketDto basket)
        {
            var redisDatabase = _redisService.GetDatabase();
            var existingBasket = await redisDatabase.StringGetAsync(basket.UserId);
            if(!existingBasket.IsNullOrEmpty)
            {
                var existingBasketDto = JsonSerializer.Deserialize<BasketDto>(existingBasket);
                if (existingBasketDto.BasketItems.Count>0)
            {
                foreach(var basketItem in basket.BasketItems)
                {
                    if(!existingBasketDto.BasketItems.Where(x=>x.CourseId== basketItem.CourseId).Any())
                    {
                        existingBasketDto.BasketItems.Add(basketItem);
                    }
                    
                }
                var result1 = await redisDatabase.StringSetAsync(basket.UserId, JsonSerializer.Serialize(existingBasketDto));
                return result1 ? ResponseDto<bool>.Success(result1, 200) : ResponseDto<bool>.Fail("Basket has not saved", 500);
            }
            }
                var result = await redisDatabase.StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));
                return result ? ResponseDto<bool>.Success(result, 200) : ResponseDto<bool>.Fail("Basket has not saved", 500);


            // StringSetAsync fonksiyonu ile ya mevcut değeri güncelle ya da yeni bir değer ekle
            



        }

        public async Task<ResponseDto<bool>> Delete(string userId)
        {
            var result = _redisService.GetDatabase().KeyDelete(userId);
            return  result ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Basket not found", 404);
        }

        public async Task<ResponseDto<bool>> DeleteElementOnBasket(string userId, string courseId)
        {
            var basket = await _redisService.GetDatabase().StringGetAsync(userId);
            if(basket.IsNullOrEmpty) return ResponseDto<bool>.Fail("Basket not found",404);
            BasketDto basketDto = JsonSerializer.Deserialize<BasketDto>(basket);
            var deleteCourse = basketDto.BasketItems.Where(x => x.CourseId == courseId).FirstOrDefault();
            if(deleteCourse == null) return ResponseDto<bool>.Fail("Course not found",404);
            basketDto.BasketItems.Remove(deleteCourse);
            var newBasket = JsonSerializer.Serialize(basketDto);
            var result =await _redisService.GetDatabase().StringSetAsync(userId,newBasket);
            return result ? ResponseDto<bool>.Success(204) : ResponseDto<bool>.Fail("Course has not removed", 500);

        }
    }
}
