using BlogProject.Web.Models;

namespace BlogProject.Web.ApiServices.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryListModel>> GetAllAsync();
        Task<List<CategoryWithBlogsCountModel>> GetAllWithBlogsCount();
        Task<CategoryListModel> GetByIdAsync(int id);
        Task AddAsync(CategoryAddModel model);
        Task UpdateAsync(CategoryUpdateModel model);
        Task DeleteAsync(int id);
    }
}
