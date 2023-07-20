using Microsoft.AspNetCore.Mvc;

namespace EduHome.App.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
