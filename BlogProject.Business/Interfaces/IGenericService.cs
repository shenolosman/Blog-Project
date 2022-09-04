using BlogProject.Entities.Interfaces;

namespace BlogProject.Business.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : class, ITable, new()
    {
        Task<List<TEntity>> GetAllAsync();
        //Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter);
        //Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter);
        //Task<List<TEntity>> GetAllAsync<Tkey>(Expression<Func<TEntity, Tkey>> keySelector);

        //Task<List<TEntity>> GetAllAsync<Tkey>(Expression<Func<TEntity, bool>> filter,
        //    Expression<Func<TEntity, Tkey>> keySelector);
        Task<TEntity> FindByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
