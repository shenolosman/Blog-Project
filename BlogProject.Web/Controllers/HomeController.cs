using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProject.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogApiService _blogApiService;

        public HomeController(ILogger<HomeController> logger, IBlogApiService blogApiService)
        {
            _logger = logger;
            _blogApiService = blogApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _blogApiService.GetAllAsync());
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            return View(await _blogApiService.GetByIdAsync(id));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}