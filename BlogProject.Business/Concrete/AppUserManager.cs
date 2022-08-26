using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.DTOs.AppUser;
using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Concrete
{
    public class AppUserManager : GenericManager<AppUser>, IAppUserService
    {
        private IGenericDal<AppUser> _genericDal;
        public AppUserManager(IGenericDal<AppUser> genericDal) : base(genericDal)
        {
            _genericDal = genericDal;
        }

        public async Task<AppUser> CheckUser(AppUserLoginDto appUserLoginDto)
        {
            return await _genericDal.GetAsync(x =>
                x.Username == appUserLoginDto.Username && x.Password == appUserLoginDto.Password);
        }
    }
}
