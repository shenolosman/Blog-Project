using BlogProject.Business.Concrete;
using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Concrete.EntityFrameworkCore;
using BlogProject.DataAccess.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlogProject.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));
        }
    }
}
