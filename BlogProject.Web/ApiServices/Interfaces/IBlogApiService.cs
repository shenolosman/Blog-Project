using BlogProject.Web.Models;

namespace BlogProject.Web.ApiServices.Interfaces
{
    public interface IBlogApiService
    {
        Task<List<BlogListModel>> GetAllAsync();
    }
}
