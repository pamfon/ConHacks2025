using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using System.Threading.Tasks;
using TBDName.Services;
using TBDName.ViewModels;

namespace TBDName.Controllers
{
	public class AIController : ParentController
	{
		public AIController(OllamaApiClient ollamaClient) : base(ollamaClient)
		{

		}

		[HttpGet]
		public IActionResult TestPage()
		{
			return View(new AITestViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> GenResponse(AITestViewModel model)
		{
			string response = await _AIService.GenResponse(model.Prompt);

			ViewBag.response = response;

			Console.WriteLine(response);
			AITestViewModel newModel = new AITestViewModel {Prompt = model.Prompt, Response = response };
			return View("TestPage", model);
		}
	}
}
