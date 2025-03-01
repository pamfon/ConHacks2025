using Microsoft.AspNetCore.Mvc;
using TBDName.Services;

namespace TBDName.Controllers
{
    public class ParentController : Controller
    {
        //static public OllamaService ollamaService = new OllamaService();
       // public AIService aiService = new AIService(ollamaService);

        public IActionResult Index()
        {
            return View();
        }
    }
}
