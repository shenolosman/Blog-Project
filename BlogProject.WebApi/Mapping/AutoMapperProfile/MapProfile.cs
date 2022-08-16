using AutoMapper;
using BlogProject.DTO.DTOs.Blog;
using BlogProject.DTO.DTOs.Category;
using BlogProject.Entities.Concrete;
using BlogProject.WebApi.Models;

namespace BlogProject.WebApi.Mapping.AutoMapperProfile
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<BlogListDto, Blog>();
            CreateMap<Blog, BlogListDto>();

            CreateMap<BlogUpdateModel, Blog>();
            CreateMap<Blog, BlogUpdateModel>();

            CreateMap<BlogAddModel, Blog>();
            CreateMap<Blog, BlogAddModel>();

            CreateMap<CategoryListDto, Category>();
            CreateMap<Category, CategoryListDto>();

            CreateMap<CategoryAddDto, Category>();
            CreateMap<Category, CategoryAddDto>();

            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryUpdateDto>();
        }
    }
}
