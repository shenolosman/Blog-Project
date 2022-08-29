using BlogProject.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignIn(AppUserLoginModel model)
        {
            return View();
        }
    }
}
