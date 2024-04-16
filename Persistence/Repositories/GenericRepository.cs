using Domain;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        protected readonly TaskForgeDbContext _forgeDbContext;
        public GenericRepository(TaskForgeDbContext forgeDbContext)
        {
            _forgeDbContext = forgeDbContext;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _forgeDbContext.Set<T>()
                .FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _forgeDbContext.Set<T>().ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _forgeDbContext
                .Set<T>()
                .FindAsync(id);
            _forgeDbContext.Set<T>().Remove(entity);
            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _forgeDbContext.Set<T>().AddAsync(entity);
            await _forgeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _forgeDbContext.Set<T>().Update(entity);
            await _forgeDbContext.SaveChangesAsync();
        }
    }
}
