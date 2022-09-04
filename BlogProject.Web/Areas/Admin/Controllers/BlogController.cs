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
    }
}
