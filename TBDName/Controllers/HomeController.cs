using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TBDName.Services;
using TBDName.Models;

namespace TBDName.Controllers
{
    public class HomeController : Controller
    {
        private readonly StorageService _storageService;

        public HomeController(StorageService storageService)
        {
            _storageService = storageService;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: Home/CheckUser
        [HttpPost]
        public IActionResult CheckUser(string userId)
        {
            // Load the users from the storage (e.g., CSV)
            var users = _storageService.LoadUsers();

            // Check if the user exists
            var existingUser = users.FirstOrDefault(u => u.UserID == userId);

            if (existingUser != null)
            {
                // Redirect to the player stats page if user exists
                return RedirectToAction("Stats", "User", new { userId = existingUser.UserID });
            }
            else
            {
                // Create a new user with the given ID
                var newUser = new User((userId));

                // Save the new user to the storage (CSV)
                _storageService.SaveUser(newUser);

                // Redirect to the player stats page for the newly created user
                return RedirectToAction("Stats", "User", new { userId = newUser.UserID });
            }
        }
    }
}
