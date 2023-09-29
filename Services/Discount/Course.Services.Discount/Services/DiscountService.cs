using Course.Services.Discount.Repository.Abstract;
using Course.SharedLibrary.Dtos;

namespace Course.Services.Discount.Services
{
    public class DiscountService:IDiscountService
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResponseDto<List<Models.Discount>>> GetAll()
        {
            var discounts = await _repository.GetAll();
            if (!discounts.Any()) return ResponseDto<List<Models.Discount>>.Fail("Any discount is not found",404);
            return ResponseDto<List<Models.Discount>>.Success(discounts,200);
        }

        public async Task<ResponseDto<Models.Discount>> GetById(int id)
        {
            var discount = await _repository.GetById(id);
            if(discount==null) return ResponseDto<Models.Discount>.Fail("Discount is not found",404);
            return ResponseDto<Models.Discount>.Success(discount,200);
        }

        public async Task<ResponseDto<Models.Discount>> GetDiscountByUserIdAndCode(string userId, string code)
        {
            var discount = await _repository.GetDiscountByUserIdAndCode(userId, code);
            if (discount == null) return ResponseDto<Models.Discount>.Fail("Discount is not found", 404);
            return ResponseDto<Models.Discount>.Success(discount, 200);
        }

        public async Task<ResponseDto<bool>> Update(Models.Discount model)
        {
            var result = await _repository.Update(model);
            return result
                ? ResponseDto<bool>.Success(201)
                : ResponseDto<bool>.Fail("an error occurred while discount updating", 500);
        }

        public async Task<ResponseDto<bool>> Delete(int id)
        {
            var result = await _repository.Delete(id);
            return result ? ResponseDto<bool>.Success(204)
                : ResponseDto<bool>.Fail("an error occurred while discount deleting", 500);
        }

        public async Task<ResponseDto<bool>> Add(Models.Discount model)
        {
            var result = await _repository.Add(model);
            return result ? ResponseDto<bool>.Success(201)
                : ResponseDto<bool>.Fail("an error occurred while discount adding", 500);
        }
    }
}
