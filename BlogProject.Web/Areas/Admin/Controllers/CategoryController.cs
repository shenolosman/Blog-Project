using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Filters;
using BlogProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [JwtAuthorize]
        public async Task<IActionResult> Index()
        {
            TempData["active"] = "category";
            return View(await _categoryService.GetAllAsync());
        }

        public IActionResult Create()
        {
            TempData["active"] = "category";
            return View(new CategoryAddModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddModel model)
        {
            TempData["active"] = "category";
            if (!ModelState.IsValid) return View(model);
            await _categoryService.AddAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            TempData["active"] = "category";
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
            TempData["active"] = "category";
            if (!ModelState.IsValid) return View(model);
            await _categoryService.UpdateAsync(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            TempData["active"] = "category";
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
