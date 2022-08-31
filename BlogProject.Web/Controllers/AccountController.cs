using BlogProject.Web.ApiServices.Interfaces;
using BlogProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(AppUserLoginModel model)
        {
            if (await _authService.SignIn(model))
            {
                return RedirectToAction("Index", "Home", new { @area = "Admin" });
            }
            return View();
        }
    }
}
