using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Interfaces
{
    public interface ICategoryService : IGenericService<Category>
    {
        Task<List<Category>> GetAllSortedByIdAsync();
    }
}
