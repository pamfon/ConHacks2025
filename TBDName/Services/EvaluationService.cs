using Philocivil.Models;

namespace Philocivil.Services
{
    public class EvaluationService
    {

        // Method to evaluate the user's answer and return the damage score
        public int EvaluateAnswer(string userAnswer, Enemy currentEnemy)
        {
            // Placeholder logic for evaluating the answer
            int score = 0;

            if (string.IsNullOrEmpty(userAnswer))
            {
                return score; // No answer given results in no damage
            }

            // Example of a basic evaluation: score the answer length
            score = Math.Min(userAnswer.Length, 10); // Limit score to a max of 10

            Console.WriteLine($"Your answer scored: {score} out of 10.");

            return score;
        }
    }
}
