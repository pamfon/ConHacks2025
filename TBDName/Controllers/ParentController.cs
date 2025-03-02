using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using Philocivil.Services;

namespace Philocivil.Controllers
{
    public class ParentController : Controller
    {
        private readonly OllamaApiClient _OllamaClient;

        public AIService _AIService;

        public ParentController(OllamaApiClient ollamaClient)
        {
            _OllamaClient = ollamaClient;
            _AIService = new AIService(_OllamaClient);
        }
    }
}
