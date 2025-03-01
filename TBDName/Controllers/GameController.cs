using Microsoft.AspNetCore.Mvc;
using TBDName.Models;
using TBDName.Services;

namespace TBDName.Controllers
{
    public class GameController : Controller
    {
        private EvaluationService _evaluationService;

        GameController(EvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        private static GameSession session = new GameSession
        (
            new User("Player1"),
            new Enemy("Goblin", 50, EnemyType.Regular)
        );

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
