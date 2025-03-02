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
			string finalPrompt = $"Instructions:\r\nPlease generate a question related to {prompt.TopicName} in {prompt.SubdivisionName} in {prompt.CountryName} and with level of {prompt.Difficulty}.\r\n\r\nContext:\r\nYou are an AI designed to create educational questions that help students study laws, governments and rights about different locations.\r\n\r\nTask/Action:\r\nProvide a concise, well-structured question that is clear and answerable within a short period.\r\nEnsure the question focuses on the location {prompt.SubdivisionName} in {prompt.CountryName} and its TOPIC without excessive detail.\r\n\r\nConstraints/Rules:\r\nKeep the response under 150 characters.\r\nAvoid bias or opinion-based questions.\r\nEnsure the question is factually accurate and relevant to the LOCATION and TOPIC.\r\nDo not provide answers to the questions only provide the questions.\r\nDo not add 'Q.' or any other formatting things to the questions. Only provide the questions themselves.\r\nDo not provide the level associated with the question, only provide the question.\r\n\r\nExamples:\r\nLevel 1 (Very Easy)\r\nQ: What is the name of the provincial capital of Ontario?\r\n\r\nLevel 2 (Easy)\r\nQ: What is the minimum legal drinking age in Ontario?\r\n\r\nLevel 3 (Medium)\r\nQ: Which Ontario law governs workplace health and safety regulations?\r\n\r\nLevel 4 (Hard)\r\nQ: In Ontario, under the Residential Tenancies Act, what is the maximum rent increase a landlord can apply without government approval?\r\n\r\nLevel 5 (Very Hard)\r\nQ: Under Ontario’s Human Rights Code, what are the five protected grounds that apply to all social areas, including employment, housing, and services?";

			return await GenResponse(finalPrompt);
		}

		//full location, difficulty, unit, extra
		public async Task<string> CreateAnswer(string prompt)
		{
			string finalPrompt = $"Instructions:\r\nPlease, consider the question {prompt}, provide me with an accurate answer.\r\n\r\nContext:\r\nYou are an AI designed to create educational answers that help students study laws, governments, and rights about different locations.\r\n\r\nTask/Action:\r\nProvide a concise, well-structured answer that directly addresses the question, ensuring it is clear and answerable within a short length. \r\nAvoid unnecessary detail.\r\nProvide short and concise sentences without losing details.\r\n\r\nConstraints/Rules:\r\n\r\nKeep the response under 150 characters.\r\nKeep answers within one - two sentences.\r\nAvoid bias or opinion-based answers.\r\nOnly provide the answer, do not add any extra formatting or insert yourself.\r\nDo not include the question in the answer, only provide the answer on its own.\r\nEnsure the answer is factually accurate and relevant to the location and topic.\r\n\r\nExamples:\r\n\r\nQ: What is the name of the provincial capital of Ontario?\r\nA: The provincial capital of Ontario is Toronto.\r\n\r\nQ: What is the minimum legal drinking age in Ontario?\r\nA: The minimum legal drinking age in Ontario is 19 years old.\r\n\r\nQ: Which Ontario law governs workplace health and safety regulations?\r\nA: The Occupational Health and Safety Act (OHSA) governs workplace health and safety regulations in Ontario.\r\n\r\nQ: In Ontario, under the Residential Tenancies Act, what is the maximum rent increase a landlord can apply without government approval?\r\nA: The maximum rent increase is set annually by the Ontario government. For 2024, it is 2.5% unless an exemption applies.\r\n\r\nQ: Under Ontario’s Human Rights Code, what are the five protected grounds that apply to all social areas, including employment, housing, and services?\r\nA: The five protected grounds under the Ontario Human Rights Code that apply to all social areas are race, ancestry, place of origin, color, and ethnic origin.";

			return await GenResponse(finalPrompt);
		}

		//returns a rank
		public async Task<int> RankAnswer(string answer, string question, string useranswer)
		{
			string finalPrompt = $"Instructions:\r\nEvaluate USERANSWER:'{useranswer}' based on QUESTION:'{question}' and the correct answer ANSWER:'{answer}', using a scale from 0 to 5, where 0 is the lowest.\r\n\r\nContext:\r\nYou are an AI designed to assess student responses in an application that helps them study laws, governments, and rights across different locations.\r\n\r\nTask/Action:\r\nAssign a score based on the accuracy and relevance of the USERANSWER compared to the ANSWER.\r\nProvide an integer score from the following scale:\r\n0 – Completely unrelated or incorrect.\r\n1 – Attempted to answer but incorrect.\r\n2 – Somewhat related but mostly incorrect.\r\n3 – Partially correct (about 50% accurate).\r\n4 – Mostly correct with minor inaccuracies.\r\n5 – Completely correct and well-stated.\r\n\r\nConstraints/Rules:\r\nThe response must be an integer (0, 1, 2, 3, 4, or 5).\r\nAvoid bias or opinion-based evaluations—base the score solely on factual accuracy and relevance.\r\n\r\nExample 1:\r\nQ: What is the minimum legal drinking age in Ontario?\r\nCorrect Answer: 19 years old\r\nUser Answer: 18 years old\r\nScore: 4.\r\n\r\nExample 2:\r\nQ: Which Ontario law governs workplace health and safety regulations?\r\nCorrect Answer: Occupational Health and Safety Act (OHSA)\r\nUser Answer: Workplace Safety Act\r\nScore: 2.\r\n\r\nExample 3:\r\nQ: What is the name of the provincial capital of Ontario?\r\nCorrect Answer: Toronto\r\nUser Answer: Vancouver\r\nScore: 0.\r\n\r\nExample 4:\r\nQ: Under Ontario’s Human Rights Code, what are the five protected grounds that apply to all social areas, including employment, housing, and services?\r\nCorrect Answer: Race, ancestry, place of origin, color, and ethnic origin.\r\nUser Answer: Race, gender, religion, age, and disability.\r\nScore: 3.\r\n\r\nExample 5:\r\nQ: In Ontario, under the Residential Tenancies Act, what is the maximum rent increase a landlord can apply without government approval?\r\nCorrect Answer: 2.5% (for 2024)\r\nUser Answer: 2.5%\r\nScore: 5.";

			string response = await GenResponse(finalPrompt);

			if (int.TryParse(response, out int val) == null)
			{
				return -1;
			}
			else 
			{
				return val;
			}
		}
	}
}