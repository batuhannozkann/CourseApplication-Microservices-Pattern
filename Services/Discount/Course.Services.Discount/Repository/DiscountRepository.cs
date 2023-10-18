using System.Data;
using Course.Services.Discount.Repository.Abstract;
using Dapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Npgsql;

namespace Course.Services.Discount.Repository
{
    public class DiscountRepository:IDiscountRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }
        public async Task<List<Models.Discount>> GetAll()
        {
            var discounts = await _connection.QueryAsync<Models.Discount>("Select * from discount");
            return discounts.ToList();
        }

        public async Task<Models.Discount> GetById(int id)
        {
            var discount = await _connection.QueryAsync<Models.Discount>("Select * from discount where id=@Id", new{Id=id});
            return discount.SingleOrDefault();
        }

        public async Task<Models.Discount> GetDiscountByUserIdAndCode(string userId, string code)
        {
            var discount = await _connection.QueryAsync< Models.Discount > (
                "Select * from Discount Where userid=@UserId and code=@Code", new { UserId = userId, Code = code });
            return discount.SingleOrDefault();
        }

        public async Task<bool> Update(Models.Discount model)
        {
            var result = await _connection.ExecuteAsync("UPDATE discount SET Rate=@rate,Code=@code,UserId=@userid where Id=@id",
                new { rate = model.Rate, code = model.Code, userid = model.UserId, id = model.Id });
            if (result > 0) return true;
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _connection.ExecuteAsync("Delete from discount Where id=@Id", new { Id = id });
            if(result > 0) return true;
            return false;
        }

        public async Task<bool> Add(Models.Discount model)
        {
            var result = await _connection.ExecuteAsync(
                "INSERT INTO discount (userid,rate,code6) VALUES (@Userid,@Rate,@Code)",
                new {Userid = model.UserId,Rate=model.Rate,Code=model.Code });
            if(result > 0) return true;
            return false;
        }
    }
}
