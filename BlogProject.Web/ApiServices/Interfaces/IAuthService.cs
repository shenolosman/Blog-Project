using BlogProject.Web.Models;

namespace BlogProject.Web.ApiServices.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignIn(AppUserLoginModel model);
    }
}
