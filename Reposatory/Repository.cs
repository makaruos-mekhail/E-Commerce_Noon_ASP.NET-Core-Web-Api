using Application.Contracts;
using Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace E_Commerce_API.Reposatories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
    {
        protected readonly DContext _context;//= new DContext();
        private readonly DbSet<TEntity> _dbSet;
        public Repository(DContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> CreateAsync(TEntity item)
        {
            var data = (await _dbSet.AddAsync(item)).Entity;
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<bool> DeleteAsync(TId id)
        {
            var item = await GetByIdAsync(id);
            if (item != null)
            {
                _dbSet.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TEntity item)
        {
            var entity = _dbSet.Update(item);

            if (entity != null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
