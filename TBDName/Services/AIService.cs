using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using System.Threading.Tasks;
using TBDName.Models;
using TBDName.ViewModels;

namespace TBDName.Services
{
	public class AIService
	{
		private readonly OllamaApiClient _OllamaClient;

		public AIService(OllamaApiClient ollamaClient)
		{
			_OllamaClient = ollamaClient;
		}

		public async Task<string> GenResponse(string prompt)
		{
			string response = "";

			await foreach (var token in _OllamaClient.GenerateAsync(prompt))
			{
				response += token.Response;
			}

			Console.WriteLine(response);
			return response;
		}

		//full location, difficulty, unit, extra
		public async Task<string> CreateQuestion(QuestionPrompt prompt)
		{
			string finalPrompt = $"";

			return await GenResponse(finalPrompt);
		}

		//full location, difficulty, unit, extra
		public async Task<string> CreateAnswer(string prompt)
		{
			string finalPrompt = $"";

			return await GenResponse(finalPrompt);
		}

		//returns a rank
		public async Task<int> RankAnswer(string prompt)
		{
			string finalPrompt = $"";

			return int.Parse(await GenResponse(finalPrompt));
		}
	}
}