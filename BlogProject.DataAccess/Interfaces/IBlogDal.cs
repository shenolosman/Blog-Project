using BlogProject.Entities.Concrete;

namespace BlogProject.DataAccess.Interfaces
{
    public interface IBlogDal : IGenericDal<Blog>
    {
        Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId);
        Task<List<Category>> GetCategoriesAsync(int blogId);
        Task<List<Blog>> GetLastFiveAsync();
    }
}
