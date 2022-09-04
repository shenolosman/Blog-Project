using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Filters;
using BlogProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(HttpClient httpClient, ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
        }
        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View(new CategoryAddModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _categoryService.AddAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var catList = await _categoryService.GetByIdAsync(id);
            return View(new CategoryUpdateModel
            {
                Id = catList.Id,
                Name = catList.Name
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _categoryService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
