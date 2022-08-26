using BlogProject.DTO.DTOs.AppUser;
using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Interfaces
{
    public interface IAppUserService : IGenericService<AppUser>
    {
        Task<AppUser> CheckUser(AppUserLoginDto appUserLoginDto);
    }
}
