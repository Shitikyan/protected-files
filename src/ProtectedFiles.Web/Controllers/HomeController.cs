using Microsoft.AspNetCore.Mvc;

namespace ProtectedFiles.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}