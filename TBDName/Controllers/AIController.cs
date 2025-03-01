using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using System.Threading.Tasks;
using TBDName.Services;
using TBDName.ViewModels;

namespace TBDName.Controllers
{
	public class AIController : Controller
	{
		private readonly OllamaApiClient _OllamaClient;

		public AIController(OllamaApiClient ollamaClient)
		{
			_OllamaClient = ollamaClient;
		}

		[HttpGet]
		public IActionResult TestPage()
		{
			return View(new AITestViewModel());
		}

		[HttpPost]
		public async Task<IActionResult> GenResponse(AITestViewModel model)
		{
			string response = "";

			await foreach (var token in _OllamaClient.GenerateAsync(model.Prompt)) 
			{
				response += token.Response;
			}
			ViewBag.response = response;

			Console.WriteLine(response);
			AITestViewModel newModel = new AITestViewModel {Prompt = model.Prompt, Response = response };
			return View("TestPage", model);
		}
	}
}
