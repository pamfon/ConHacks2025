using System;
using TBDName.Services;

namespace TBDName.Models
{
    public class GameSession
    {
        // Properties to track the current state of the game
        public User Player { get; set; }
        public Enemy CurrentEnemy { get; set; }
        public int QuestionsAsked { get; set; }
        public int CumulativeDamage { get; set; } // Tracks total damage dealt to the enemy

        // Constructor to initialize a new game session
        public GameSession(User player, Enemy enemy)
        {
            Player = player;
            CurrentEnemy = enemy;
            QuestionsAsked = 0;
            CumulativeDamage = 0;
        }

        // Method to handle a player's answer and calculate damage
        public void AnswerQuestion(string userAnswer, EvaluationService evaluationService)
        {
            // Evaluate the player's answer
            int damage = evaluationService.EvaluateAnswer(userAnswer, CurrentEnemy);
            CumulativeDamage += damage;

            // Inflict the calculated damage to the enemy
            CurrentEnemy.TakeDamage(damage);

            // Check if the enemy is defeated
            if (CurrentEnemy.IsDefeated())
            {
                Console.WriteLine($"{CurrentEnemy.Name} is defeated!");
                Player.GainXP(100); // Example XP reward
                ResetRound();
            }
        }

        // Method to increment the number of questions asked during the round
        public void AskQuestion()
        {
            if (CurrentEnemy is Boss boss && QuestionsAsked >= boss.MaxQuestions)
            {
                Console.WriteLine("The boss has reached the max question limit!");
                return;
            }

            QuestionsAsked++;
            Console.WriteLine($"Question {QuestionsAsked}: {CurrentEnemy.GetStatus()}");
        }

        // Method to reset the round when an enemy is defeated
        private void ResetRound()
        {
            // Reset the game state for the next round (if any)
            QuestionsAsked = 0;
            CumulativeDamage = 0;
        }

        // Method to get the current session status
        public string GetSessionStatus()
        {
            return $"{Player.GetStatus()} | Enemy: {CurrentEnemy.GetStatus()} | Questions Asked: {QuestionsAsked}";
        }
    }
}
