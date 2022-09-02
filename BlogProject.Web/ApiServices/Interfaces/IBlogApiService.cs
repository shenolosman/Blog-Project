using BlogProject.Web.Models;

namespace BlogProject.Web.ApiServices.Interfaces
{
    public interface IBlogApiService
    {
        Task<List<BlogListModel>> GetAllAsync();
        Task<BlogListModel> GetByIdAsync(int id);
        Task<List<BlogListModel>> GetAllByCategoryIdAsync(int id);
        Task AddAsync(BlogAddModel model);
    }
}
