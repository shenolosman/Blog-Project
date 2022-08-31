using BlogProject.Business.Interfaces;
using BlogProject.Entities.Concrete;

namespace BlogProject.WebApi
{
    public class JwtIdentityInitializer
    {
        public static async Task Seed(IAppUserService appUserService)
        {
            var adminUser = await appUserService.FindByNameAsync("senol");
            if (adminUser == null)
            {
                await appUserService.AddAsync(new AppUser
                {
                    Username = "senol",
                    Password = "Passw0rd!",
                    Name = "senol",
                    Surname = "senolov",
                    Email = "senol@mail.com"
                });

            }
        }
    }
}
