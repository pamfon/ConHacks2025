using Microsoft.AspNetCore.Mvc;
using OllamaSharp;
using Philocivil.Models;
using System.Threading.Tasks;
using Philocivil.ViewModels;

namespace Philocivil.Services
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
            string finalPrompt = $"Instructions:\r\n\r\nEvaluate the USERANSWER:'{useranswer}' to the given QUESTION:'{question}' by comparing it against the CORRECTANSWER:'{answer}'. Assign a score based on how accurate and relevant the USERANSWER is in relation to the CORRECTANSWER.\r\nScoring Scale:\r\n\r\n    0 – Completely unrelated or entirely incorrect.\r\n    1 – Attempted to answer but is mostly incorrect.\r\n    2 – Somewhat related but contains significant inaccuracies.\r\n    3 – Partially correct (approximately 50% accurate).\r\n    4 – Mostly correct with minor errors.\r\n    5 – Almost completely correct, with only slight inaccuracies.\r\n\r\nEvaluation Criteria:\r\n\r\n    Consider factual accuracy—does the USERANSWER contain correct information?\r\n    Consider completeness—does it address all key points of the CORRECTANSWER?\r\n    Consider clarity and relevance—is the answer on-topic and properly stated?\r\n\r\nConstraints/Rules:\r\n\r\n    The response must be a single integer: 0, 1, 2, 3, 4, or 5 (no explanations).\r\nDo not provide anything but a single number.\r\nOnly provide a number and nothing else.\r\n    Do not provide reasoning or feedback, only the integer score.\r\n    Base the score strictly on accuracy and relevance, avoiding subjective judgment.\r\n\r\nExample Format:\r\n\r\n1";

            string response = await GenResponse(finalPrompt);
            Console.WriteLine(response);
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