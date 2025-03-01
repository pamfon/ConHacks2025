using Microsoft.AspNetCore.Mvc;

namespace TBDName.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
