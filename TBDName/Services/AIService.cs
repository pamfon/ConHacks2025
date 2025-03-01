using System.Threading.Tasks;

namespace TBDName.Services
{
	public class AIService
	{
		private readonly OllamaService _ollamaService;

		// Inject the OllamaService via constructor
		public AIService(OllamaService ollamaService)
		{
			_ollamaService = ollamaService;
		}

		// Asks the AI for a response based on a string
		public async Task<string> GenerateResponse(string input)
		{
			// Call the OllamaService to get the AI's response
			return await _ollamaService.GetAIResponseAsync(input);
		}
	}
}