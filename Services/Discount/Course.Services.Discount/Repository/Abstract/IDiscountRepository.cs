namespace Course.Services.Discount.Repository.Abstract
{
    public interface IDiscountRepository
    {
        Task<List<Models.Discount>> GetAll();
        Task<Models.Discount> GetById(int id);
        Task<Models.Discount> GetDiscountByUserIdAndCode(string userId, string code);
        Task<bool> Update (Models.Discount model);
        Task<bool> Delete(int id);
        Task<bool> Add(Models.Discount model);
    }
}
