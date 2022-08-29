using BlogProject.Web.ApiServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Web.ViewComponents
{
    public class GetCategoryName : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public GetCategoryName(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IViewComponentResult Invoke(int categoryId)
        {

            return View(_categoryService.GetByIdAsync(categoryId).Result);
        }
    }
}
