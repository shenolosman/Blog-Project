using BlogProject.Web.Models;

namespace BlogProject.Web.ApiServices.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryListModel>> GetAllAsync();
    }
}
