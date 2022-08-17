using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.Entities.Interfaces;

namespace BlogProject.Business.Concrete
{
    public class GenericManager<TEntity> : IGenericService<TEntity> where TEntity : class, ITable, new()
    {
        private readonly IGenericDal<TEntity> _genericDal;

        public GenericManager(IGenericDal<TEntity> genericDal)
        {
            _genericDal = genericDal;
        }
        public async Task<List<TEntity>> GelAllAsync()
        {
            return await _genericDal.GelAllAsync();
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _genericDal.FinByIdAsync(id);
        }

        //public async Task<List<TEntity>> GelAllAsync(Expression<Func<TEntity, bool>> filter)
        //{
        //    return await _genericDal.GelAllAsync(filter);
        //}

        //public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        //{
        //    return await _genericDal.GetAsync(filter);
        //}

        //public async Task<List<TEntity>> GelAllAsync<Tkey>(Expression<Func<TEntity, Tkey>> keySelector)
        //{
        //    return await _genericDal.GelAllAsync(keySelector);
        //}

        //public async Task<List<TEntity>> GelAllAsync<Tkey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, Tkey>> keySelector)
        //{
        //    return await _genericDal.GelAllAsync(filter, keySelector);
        //}

        public async Task AddAsync(TEntity entity)
        {
            await _genericDal.AddAsync(entity);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await _genericDal.UpdateAsync(entity);
        }

        public async Task RemoveAsync(TEntity entity)
        {
            await _genericDal.RemoveAsync(entity);
        }
    }
}
