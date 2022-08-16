using BlogProject.Entities.Interfaces;

namespace BlogProject.Business.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : class, ITable, new()
    {
        Task<List<TEntity>> GelAllAsync();
        //Task<List<TEntity>> GelAllAsync(Expression<Func<TEntity, bool>> filter);
        //Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        //Task<List<TEntity>> GelAllAsync<Tkey>(Expression<Func<TEntity, Tkey>> keySelector);

        //Task<List<TEntity>> GelAllAsync<Tkey>(Expression<Func<TEntity, bool>> filter,
        //    Expression<Func<TEntity, Tkey>> keySelector);
        Task<TEntity> FinByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
