using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TBDName.Services;
using TBDName.Models;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using TBDName.ViewModels;

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
                // Redirect to the country selection page
                return RedirectToAction("SelectCountry");
            }
            else
            {
                // Create a new user with the given ID
                var newUser = new User((userId));

                // Save the new user to the storage (CSV)
                _storageService.SaveUser(newUser);

                // Redirect to the country selection page
                return RedirectToAction("SelectCountry");
            }
        }

        // The country selection page
        public IActionResult SelectCountry()
        {
            var countries = _storageService.LoadCountries();

			return View(countries);
        }

        public IActionResult SelectLevel(string countryId)
        {
            var countries = _storageService.LoadCountries();
            var country = countries.FirstOrDefault(c => c.Id == countryId);

            if (country == null) { return NotFound(); }

			var subdivisions = _storageService.LoadSubdivisions(countryId);

            var model = new SelectLevelViewModel
            {
                Country = country,
                Subdivisions = subdivisions
            };


            return View(model);
        }

        
        public IActionResult SelectTopic(string countryId, string? subdivisionId = null)
        {
            var countries = _storageService.LoadCountries();
            var country = countries.FirstOrDefault(c => c.Id == countryId);

            if (country == null ) { return NotFound(); }

            Subdivision subdivision = null;
            
            if (subdivisionId != null)
            {
                var subdivisions = _storageService.LoadSubdivisions(countryId);
                subdivision = subdivisions.FirstOrDefault(s => s.Id == subdivisionId);
            }
 
            var topics = _storageService.LoadTopics();

            var model = new SelectTopicViewModel
            {
                Country = country,
                Subdivision = subdivision,
                Topics = topics
            };

            return View(model);
        }
    }
}
