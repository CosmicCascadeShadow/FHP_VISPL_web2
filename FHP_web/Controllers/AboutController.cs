using Microsoft.AspNetCore.Mvc;

namespace FHP_web.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("loginUser") != null)
            {
                ViewBag.loginUser = HttpContext.Session.GetString("loginUser").ToString();
            }

            return View();
        }
    }
}
