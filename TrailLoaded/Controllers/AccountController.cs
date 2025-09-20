using Microsoft.AspNetCore.Mvc;

namespace TrailLoaded.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
