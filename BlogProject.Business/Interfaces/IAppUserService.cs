using BlogProject.DTO.DTOs.AppUser;
using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> CheckUserAsync(AppUserLoginDto appUserLoginDto);
        Task<AppUser> FindByNameAsync(string username);
    }
}
