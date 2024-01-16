using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Order.Core.Assigments;
using Order.Infrastructure.Contexts;

namespace Order.Infrastructure.Repositories
{
    public class BaseRepository<TEntity,TContext>
    where TEntity : Entity
    where TContext:DbContext
    {
        private TContext _context;

        public BaseRepository(TContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> Table => _context.Set<TEntity>();
        public async Task<TEntity> GetAsync(Expression<Func<TEntity,bool>> expression)
        {
            return await Table.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }
        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            Table.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            Table.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            Table.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }


    }
}
