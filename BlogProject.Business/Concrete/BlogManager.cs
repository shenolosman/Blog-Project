using BlogProject.Business.Interfaces;
using BlogProject.DataAccess.Interfaces;
using BlogProject.DTO.DTOs.CategoryBlog;
using BlogProject.Entities.Concrete;

namespace BlogProject.Business.Concrete
{
    public class BlogManager : GenericManager<Blog>, IBlogService
    {
        private readonly IGenericDal<Blog> _genericDal;
        private readonly IBlogDal _blogDal;
        private readonly IGenericDal<CategoryBlog> _categoryBlogService;

        public BlogManager(IGenericDal<Blog> genericDal, IBlogDal blogDal, IGenericDal<CategoryBlog> categoryBlogService) : base(genericDal)
        {
            _genericDal = genericDal;
            _blogDal = blogDal;
            _categoryBlogService = categoryBlogService;
        }

        public async Task<List<Blog>> GetAllSortedByPostedTimeAsync()
        {
            return await _genericDal.GetAllAsync(I => I.PostedTime);
        }

        public async Task AddToCategoryAsync(CategoryBlogDto categoryBlogDto)
        {
            var control = await _categoryBlogService.GetAsync(I => I.CategoryId == categoryBlogDto.CategoryId && I.BlogId == categoryBlogDto.BlogId);
            if (control == null)
            {
                await _categoryBlogService.AddAsync(new CategoryBlog
                {
                    BlogId = categoryBlogDto.BlogId,
                    CategoryId = categoryBlogDto.CategoryId
                });
            }

        }
        public async Task RemoveFromCategoryAsync(CategoryBlogDto categoryBlogDto)
        {
            var deletedCategoryBlog = await _categoryBlogService.GetAsync(I => I.CategoryId == categoryBlogDto.CategoryId && I.BlogId == categoryBlogDto.BlogId);
            if (deletedCategoryBlog != null)
            {
                await _categoryBlogService.RemoveAsync(deletedCategoryBlog);
            }


        }

        public async Task<List<Blog>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await _blogDal.GetAllByCategoryIdAsync(categoryId);
        }

    }
}
