using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories
{
    public class EfGenericRepository<TEntity> : IGenericDal<TEntity> where TEntity : class, ITable, new()
    {
        public async Task<List<TEntity>> GelAllAsync()
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GelAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<List<TEntity>> GelAllAsync<Tkey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, Tkey>> keySelector)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().Where(filter).OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<TEntity> FinByIdAsync(int id)
        {
            await using var context = new DatabaseContext();
            return await context.FindAsync<TEntity>(id);
        }

        public async Task<List<TEntity>> GelAllAsync<Tkey>(Expression<Func<TEntity, Tkey>> keySelector)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().OrderByDescending(keySelector).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new DatabaseContext();
            return await context.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public async Task AddAsync(TEntity entity)
        {
            await using var context = new DatabaseContext();
            await context.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await using var context = new DatabaseContext();
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await using var context = new DatabaseContext();
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
