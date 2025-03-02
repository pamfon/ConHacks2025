using Microsoft.AspNetCore.Mvc;
using Philocivil.Services;

namespace Philocivil.Controllers
{
    public class UserController : Controller
    {
        private readonly StorageService _storageService;

        public UserController(StorageService storageService)
        {
            _storageService = storageService;
        }

        // GET: User/Stats/{userId}
        public IActionResult Stats(string userId)
        {
            // Assuming userId is used to retrieve the user data
            var user = _storageService.LoadUsers().FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                // Handle the case when the user doesn't exist (optional)
                return RedirectToAction("Index", "Home");
            }

            return View(user); // Pass the user model to the view
        }

    }
}

