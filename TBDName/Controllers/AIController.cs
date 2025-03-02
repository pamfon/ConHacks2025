using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using Philocivil.Models;
using Philocivil.ViewModels;
using System.Threading.Tasks;
using Philocivil.Services;

namespace Philocivil.Controllers
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
            QuestionPrompt question = new QuestionPrompt
            {
                SubdivisionName = "Rio Grande do Norte",
                CountryName = "Brazil",
                Difficulty = 5,
                TopicName = "Rights"
            };

            string response = await _AIService.CreateQuestion(question);
            string answer = await _AIService.CreateAnswer(response);
            ViewBag.response = response;
            ViewBag.answer = answer;

            int temp = -1;

            while (temp == -1)
            {
                temp = await _AIService.RankAnswer(answer, response, answer);
            }

            ViewBag.rank = temp;
            AITestViewModel newModel = new AITestViewModel { Prompt = model.Prompt, Response = response };
            return View("TestPage", model);
        }
    }
}
