using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Filters;
using BlogProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogApiService _blogApiService;

        public BlogController(IBlogApiService blogApiService)
        {
            _blogApiService = blogApiService;
        }
        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "blog";
            return View(await _blogApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            TempData["active"] = "blog";
            return View(new BlogAddModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            TempData["active"] = "blog";
            if (!ModelState.IsValid) return View(model);
            await _blogApiService.AddAsync(model);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "blog";
            var blogList = await _blogApiService.GetByIdAsync(id);
            return View(new BlogUpdateModel
            {
                Id = blogList.Id,
                ShortDescription = blogList.ShortDescription,
                Description = blogList.Description,
                Title = blogList.Title
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateModel model)
        {
            TempData["active"] = "blog";
            if (!ModelState.IsValid) return View(model);
            await _blogApiService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "blog";
            await _blogApiService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AssignCategory(int id, [FromServices] ICategoryService categoryService)
        {
            TempData["active"] = "blog";
            var categories = await categoryService.GetAllAsync();
            var blogCategories = await _blogApiService.GetCategoriesAsync(id)/*).Select(x => x.Name).ToList()*/;

            TempData["blogId"] = id;

            List<AssignCategoryModel> list = new List<AssignCategoryModel>();

            foreach (var category in categories)
            {
                AssignCategoryModel model = new AssignCategoryModel();
                model.CategoryId = category.Id;
                model.CategoryName = category.Name;
                model.Exists = blogCategories.Contains(category/*.Name*/);

                list.Add(model);
            }

            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> AssignCategory(List<AssignCategoryModel> list)
        {
            int id = (int)TempData["blogId"];
            foreach (var item in list)
            {
                if (item.Exists)
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;
                    await _blogApiService.AddToCategoryAsync(model);
                }
                else
                {
                    CategoryBlogModel model = new CategoryBlogModel();
                    model.BlogId = id;
                    model.CategoryId = item.CategoryId;
                    await _blogApiService.RemoveFromCategoryAsync(model);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
