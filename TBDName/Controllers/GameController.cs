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

        GameController(EvaluationService evaluationService, OllamaApiClient ollamaClient) : base(ollamaClient)

		{
            _evaluationService = evaluationService;
        }

        private static GameSession session = new GameSession
        (
            new User("Player1"),
            new Enemy("Goblin", 50, EnemyType.Regular)
        );

        [HttpGet("/Index{Id}")]
        public async Task<IActionResult> Index(int Id, SelectTopicViewModel lastViewModel, string topicName) 
        {
            GameViewModel gameModel = new GameViewModel
            {
                Level = Id,
                Country = lastViewModel.Country.Name,
                Subdivision = lastViewModel.Subdivision.Name ?? "Federal",
                Topic = topicName,
                GameSession = new GameSession()
            };

            QuestionPrompt quest = new QuestionPrompt
            {
                CountryName = gameModel.Country,
                SubdivisionName = gameModel.Subdivision,
                Difficulty = gameModel.Level,
                TopicName = gameModel.Topic
            };

            gameModel.GameSession.Question = await _AIService.CreateQuestion(quest);

			return View("Index", gameModel);
		}

        [HttpPost]
        public IActionResult SubmitAnswer(string userAnswer)
        {
            session.AnswerQuestion(userAnswer, _evaluationService);

            ViewBag.Status = session.GetSessionStatus();
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
