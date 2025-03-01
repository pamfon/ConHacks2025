using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class OllamaService
{
	private readonly HttpClient _httpClient;

	public OllamaService(HttpClient httpClient)
	{
		_httpClient = httpClient;
		_httpClient.BaseAddress = new Uri("http://localhost:11434/"); // Ensure Ollama is running on this port
	}

	public async Task<string> GetAIResponseAsync(string prompt)
	{
		var requestBody = new
		{
			model = "llama3.1:8b", // Ensure this is correct
			prompt = prompt,
			max_tokens = 150
		};

		var json = JsonSerializer.Serialize(requestBody);
		var content = new StringContent(json, Encoding.UTF8, "application/json");

		var response = await _httpClient.PostAsync("api/generate", content);

		if (response.IsSuccessStatusCode)
		{
			var responseJson = await response.Content.ReadAsStringAsync();
			var responseObject = JsonSerializer.Deserialize<OllamaResponse>(responseJson);
			return responseObject?.Choices[0]?.Text ?? "No response from AI.";
		}

		return $"Error: {response.StatusCode}";
	}

	private class OllamaResponse
	{
		public Choice[] Choices { get; set; }
	}

	private class Choice
	{
		public string Text { get; set; }
	}
}
