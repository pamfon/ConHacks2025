using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OllamaSharp;
using OllamaSharp.Models;
using Philocivil.Models;
using Philocivil.Services;
using Philocivil.ViewModels;
using System.Text.Json;
using Philocivil.Models;

namespace Philocivil.Controllers
{
    public class GameController : ParentController
    {
        private readonly StorageService _storageService;

        public GameController(StorageService storageService, OllamaApiClient ollamaClient) : base(ollamaClient)
        {
            _storageService = storageService;
        }

        // Configure the serializer to include fields
        private JsonSerializerOptions _jsonOptions = new JsonSerializerOptions { IncludeFields = true };

        [HttpGet]
        public async Task<IActionResult> Index(string topic, int level, string country, string subdivision)
        {
            GameViewModel gameModel = new GameViewModel
            {
                Level = level,
                Country = country,
                Subdivision = subdivision ?? "Federal",
                Topic = topic,
                GameSession = new GameSession()
            };

            switch (level)
            {
                case 1:
                    gameModel.Path = Url.Content("~/Images/Blue-Slime.png");
                    break;
                case 2:
                    gameModel.Path = Url.Content("~/Images/Red-Slime.png");
                    break;
                case 3:
                    gameModel.Path = Url.Content("~/Images/King-Slime.png");
                    break;
                case 4:
                    gameModel.Path = Url.Content("~/WorldGifs/Goblin.gif");
                    break;
                case 5:
                    gameModel.Path = Url.Content("~/WorldGifs/RedGoblin.gif");
                    break;
                case 6:
                    gameModel.Path = Url.Content("~/Images/dagon.png");
                    break;
            }

            Random rand = new Random();
            int temp = rand.Next(3, 6);

            QuestionPrompt quest = new QuestionPrompt
            {
                CountryName = gameModel.Country,
                SubdivisionName = gameModel.Subdivision,
                Difficulty = gameModel.Level == 6 ? temp : gameModel.Level,
                TopicName = gameModel.Topic
            };
            gameModel.GameSession.enemyHealth = gameModel.Level == 6 ? 15 : 15;
            gameModel.GameSession.Question = await _AIService.CreateQuestion(quest);

            HttpContext.Session.SetObject("game", gameModel);

            return View("Index", gameModel);
        }

        [HttpPost]
        public async Task<IActionResult> Turn(GameViewModel game)
        {
            // Preserve the posted answer before retrieving the persisted game state
            string postedAnswer = game.GameSession == null ? "" : game.GameSession.AnswerGiven;

            game = HttpContext.Session.GetObject<GameViewModel>("game");
            game.GameSession.AnswerGiven = postedAnswer;

            // Toggle the game state (e.g., switching between question and answer phases)
            game.GameSession.GameState = !game.GameSession.GameState;
            if (game.GameSession.GameState) { game.GameSession.round++; }

            QuestionPrompt quest = new QuestionPrompt
            {
                CountryName = game.Country,
                SubdivisionName = game.Subdivision,
                Difficulty = game.Level,
                TopicName = game.Topic
            };
            if (game.Level != 6 && game.GameSession.enemyHealth > 0)
            {
                if (game.GameSession.GameState)
                {
                    // If in answer state, create answer and rank it
                    game.GameSession.Feedback = await _AIService.CreateAnswer(game.GameSession.Question);
                    game.GameSession.enemyHealth -= await _AIService.RankAnswer(
                        game.GameSession.Feedback,
                        game.GameSession.Question,
                        game.GameSession.AnswerGiven
                    );

                    if (game.GameSession.enemyHealth < 0)
                    {
                        game.GameSession.enemyHealth = 0;
                    }
                }
                else
                {
                    // Otherwise, generate a new question
                    game.GameSession.Question = await _AIService.CreateQuestion(quest);
                }
            }
            else if (game.Level == 6 && game.GameSession.enemyHealth > 0 && game.GameSession.round <= 5)
            {
                if (game.GameSession.GameState)
                {
                    // If in answer state, create answer and rank it
                    game.GameSession.Feedback = await _AIService.CreateAnswer(game.GameSession.Question);
                    game.GameSession.enemyHealth -= await _AIService.RankAnswer(
                        game.GameSession.Feedback,
                        game.GameSession.Question,
                        game.GameSession.AnswerGiven
                    );

                    if (game.GameSession.enemyHealth < 0)
                    {
                        game.GameSession.enemyHealth = 0;
                    }
                }
                else
                {
                    // Otherwise, generate a new question
                    Random rand = new Random();
                    quest.Difficulty = rand.Next(3, 6);
                    game.GameSession.Question = await _AIService.CreateQuestion(quest);
                }
            }
            else
            {
                var countries = _storageService.LoadCountries();
                var country = countries.FirstOrDefault(c => c.Name == game.Country);
                Console.WriteLine(game.Subdivision);
                var subdivisions = _storageService.LoadSubdivisions(country.Id);
                var subdivision = subdivisions.FirstOrDefault(s => s.Name == game.Subdivision);

                // Load available topics
                var topics = _storageService.LoadTopics();

                SelectTopicViewModel model = new SelectTopicViewModel
                {
                    Country = country,
                    Subdivision = subdivision,
                    Topics = topics
                };
                return RedirectToAction("SelectTopic", "Home", new { countryId = country.Id, subdivisionId = subdivision.Id });
            }

            // Update TempData for persistence
            HttpContext.Session.SetObject("game", game);

            return View("Index", game);
        }
    }
}