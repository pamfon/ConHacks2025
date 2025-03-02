using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Philocivil.ViewModels;

namespace Philocivil.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Philocivil.Models;
    using System.Linq;
    using Philocivil.Models;
    using Philocivil.Services;

    /// <summary>
    /// Controller responsible for handling navigation in the application, including user authentication,
    /// country selection, level selection (federal/provincial), and topic selection.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly StorageService _storageService;

        /// <summary>
        /// Initializes the HomeController with a StorageService dependency.
        /// </summary>
        /// <param name="storageService">Service responsible for handling data persistence.</param>
        public HomeController(StorageService storageService)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// Displays the main landing page where the user can enter their User ID.
        /// </summary>
        /// <returns>The Index view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles user authentication. If the user exists, they are redirected to country selection;
        /// otherwise, a new user is created and saved, then redirected to country selection.
        /// </summary>
        /// <param name="userId">The ID of the user attempting to log in.</param>
        /// <returns>Redirects to the country selection page.</returns>
        [HttpPost]
        public IActionResult CheckUser(string userId)
        {
            // Load the users from storage (e.g., CSV)
            var users = _storageService.LoadUsers();

            // Check if the user exists in the system
            var existingUser = users.FirstOrDefault(u => u.UserID == userId);

            if (existingUser != null)
            {
                // Redirect to country selection if the user exists
                return RedirectToAction("SelectCountry");
            }
            else
            {
                // Create and save a new user with the provided ID
                var newUser = new User(userId);
                _storageService.SaveUser(newUser);

                // Redirect to country selection after creating a new user
                return RedirectToAction("SelectCountry");
            }
        }

        /// <summary>
        /// Displays the country selection page, allowing the user to choose a country to play in.
        /// </summary>
        /// <returns>The SelectCountry view populated with the list of available countries.</returns>
        public IActionResult SelectCountry()
        {
            var countries = _storageService.LoadCountries();
            return View(countries);
        }

        /// <summary>
        /// Displays the level selection page for a chosen country. The user can choose to play at the
        /// federal level or select a specific province/state.
        /// </summary>
        /// <param name="countryId">The ID of the selected country.</param>
        /// <returns>The SelectLevel view populated with country and subdivision data.</returns>
        public IActionResult SelectLevel(string countryId)
        {
            var countries = _storageService.LoadCountries();
            var country = countries.FirstOrDefault(c => c.Id == countryId);

            if (country == null)
            {
                return NotFound();
            }

            // Load the subdivisions (provinces/states) for the selected country
            var subdivisions = _storageService.LoadSubdivisions(countryId);

            var model = new SelectLevelViewModel
            {
                Country = country,
                Subdivisions = subdivisions
            };

            return View(model);
        }

        /// <summary>
        /// Displays the topic selection page after the user has chosen a country and (if applicable) a subdivision.
        /// Topics represent different subjects (e.g., Governance, Rights, Laws).
        /// </summary>
        /// <param name="countryId">The ID of the selected country.</param>
        /// <param name="subdivisionId">The optional ID of the selected subdivision (province/state).</param>
        /// <returns>The SelectTopic view populated with available topics for the given country/subdivision.</returns>
        public IActionResult SelectTopic(string countryId, string? subdivisionId)
        {
            Console.WriteLine("Made it to select");

            var countries = _storageService.LoadCountries();
            var country = countries.FirstOrDefault(c => c.Id == countryId);

            if (country == null)
            {
                return NotFound();
            }

            Subdivision subdivision = null;

            if (subdivisionId != null)
            {
                var subdivisions = _storageService.LoadSubdivisions(countryId);
                subdivision = subdivisions.FirstOrDefault(s => s.Id == subdivisionId);
            }

            // Load available topics
            var topics = _storageService.LoadTopics();

            var model = new SelectTopicViewModel
            {
                Country = country,
                Subdivision = subdivision ?? new Subdivision(),
                Topics = topics
            };

            return View(model);
        }
    }

}
