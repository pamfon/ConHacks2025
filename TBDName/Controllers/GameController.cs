using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using TBDName.Models;
using TBDName.Services;
using TBDName.ViewModels;

namespace TBDName.Controllers
{
    public class GameController : ParentController
    {
        private EvaluationService _evaluationService;
        private StorageService _storageService;

		public GameController(OllamaApiClient ollamaClient, EvaluationService evaluationService, StorageService storageService) : base(ollamaClient)
		{
			_evaluationService = evaluationService;
            _storageService = storageService;
        }

        private GameSession GetOrCreateGameSession()
        {
            if (HttpContext.Session.GetObject<GameSession>("GameSession") == null)
            {
                User player = 
            }
        }

        public IActionResult Index()
        {
            var model = new GameBattleViewModel
            {
                Player = _session.Player,
            };
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAnswer(string userAnswer)
        {
            int score = await _AIService.RankAnswer(userAnswer);
            string answer = await _AIService.CreateAnswer(userAnswer);

            return View("Index");
        }

        [HttpPost]
        public IActionResult NextQuestion(string questionText, string countryName, string subdivisionName,  string topic, int difficulty)
        {
            return View("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
