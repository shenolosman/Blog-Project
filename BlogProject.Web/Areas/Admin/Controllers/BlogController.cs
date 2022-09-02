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
            return View(await _blogApiService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View(new BlogAddModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogAddModel model)
        {
            if (ModelState.IsValid)
            {
                await _blogApiService.AddAsync(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
