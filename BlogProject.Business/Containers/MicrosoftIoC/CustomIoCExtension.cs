using BlogProject.Business.Concrete;
using BlogProject.Business.Interfaces;
using BlogProject.Business.Tools.JwtTool;
using BlogProject.Business.Tools.LogTool;
using BlogProject.Business.ValidationRules.FluentValidation;
using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Context;
using BlogProject.DataAccess.Concrete.EntityFrameworkCore.Repositories;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.DTOs.AppUser;
using BlogProject.DTO.DTOs.Category;
using BlogProject.DTO.DTOs.CategoryBlog;
using BlogProject.DTO.DTOs.Comment;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BlogProject.Business.Containers.MicrosoftIoC
{
    public static class CustomIoCExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>();

            services.AddScoped(typeof(IGenericDal<>), typeof(EfGenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));


            services.AddScoped<IBlogService, BlogManager>();
            services.AddScoped<IBlogDal, EfBlogRepository>();

            services.AddScoped<IAppUserService, AppUserManager>();
            services.AddScoped<IAppUserDal, EfAppUserRepository>();

            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICategoryDal, EfCategoryRepository>();

            services.AddScoped<ICommentService, CommentManager>();
            services.AddScoped<ICommentDal, EfCommentRepository>();

            services.AddScoped<IJwtService, JwtManager>();
            services.AddScoped<ICustomLogger, NLogAdapter>();

            services.AddTransient<IValidator<AppUserLoginDto>, AppUserLoginValidator>();
            services.AddTransient<IValidator<CategoryAddDto>, CategoryAddValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();
            services.AddTransient<IValidator<CategoryBlogDto>, CategoryBlogValidator>();
            services.AddTransient<IValidator<CommentAddDto>, CommentAddValidator>();
        }
    }
}
