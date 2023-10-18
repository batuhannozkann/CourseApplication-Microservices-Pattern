using Microsoft.EntityFrameworkCore;
using Order.Core.Assigments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Repositories
{
    public interface IBaseRepository<TEntity>
    where TEntity : class
    {
        public  Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        public  Task<IEnumerable<TEntity>> GetAllAsync();
        public  Task<TEntity> UpdateAsync(TEntity entity);
        public  Task<TEntity> DeleteAsync(TEntity entity);
        public Task<TEntity> AddAsync(TEntity entity);
    }
}
