using System;
using TBDName.Services;

namespace TBDName.Models
{
    public class GameSession
    {
        // Properties to track the current state of the game
      //  public User Player { get; set; }
     //   public Enemy CurrentEnemy { get; set; }
        public string Question { get; set; }
        public string Feedback { get; set; }
        public string AnswerGiven { get; set; }
        public int CumulativeDamage { get; set; } // Tracks total damage dealt to the enemy
        //used to check what score overall the player got
        public int score { get; set; }

        //used to store enemy health
        public int enemyHealth { get; set; }

        //used to record which round youre on
        public int round { get; set; }

        public bool GameState { get; set; }

        // Constructor to initialize a new game session
        public GameSession(User player, Enemy enemy)
        {
        //    Player = player;
         //   CurrentEnemy = enemy;
            Question = "";
            CumulativeDamage = 0;
        }

        public GameSession()
        {
        //    Player = new User("");
        //    CurrentEnemy = new Enemy("", 20, EnemyType.Regular);
            Question = "";
            Feedback = "";
            AnswerGiven = "";
            CumulativeDamage = 0;
        }

        // Method to handle a player's answer and calculate damage
      /*  public void AnswerQuestion(string userAnswer, EvaluationService evaluationService)
        {
            // Evaluate the player's answer
            int damage = evaluationService.EvaluateAnswer(userAnswer, CurrentEnemy);
            CumulativeDamage += damage;

            // Inflict the calculated damage to the enemy
            CurrentEnemy.TakeDamage(damage);

            // Check if the enemy is defeated
            if (CurrentEnemy.IsDefeated())
            {
                if (CurrentEnemy.Type == EnemyType.Boss)
                {
                    Player.GainXP(50);
                } 
                else
                {
					Player.GainXP(20);
				}

                ResetRound();
            }
        }

        // Method to increment the number of questions asked during the round
        public void AskQuestion()
        {
          /*  if (CurrentEnemy is Boss boss && QuestionsAsked >= boss.MaxQuestions)
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
            Question = "";
            CumulativeDamage = 0;
        }

        // Method to get the current session status
        public string GetSessionStatus()
        {
            return $"{Player.GetStatus()} | Enemy: {CurrentEnemy.GetStatus()} | Questions Asked: {Question}";
        }
        */
    }
}
