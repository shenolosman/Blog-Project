using Microsoft.AspNetCore.Mvc;

namespace BlogProject.Web.ViewComponents
{
    public class Search : ViewComponent
    {
        public IViewComponentResult Invoke(string s)
        {
            ViewBag.SearchString = s;
            return View();
        }
    }
}
