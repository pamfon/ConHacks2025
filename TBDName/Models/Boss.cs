using System;
using System.Xml.Linq;

namespace Philocivil.Models
{
    public class Boss : Enemy
    {
        // Additional properties for the boss
        public int MaxQuestions { get; set; } // Max number of questions to defeat the boss
        public int QuestionsAsked { get; set; } // Tracks the number of questions asked during the battle

        // Constructor to initialize the boss
        public Boss(string name, int maxHP, int maxQuestions)
            : base(name, maxHP, EnemyType.Boss)
        {
            MaxQuestions = maxQuestions;
            QuestionsAsked = 0;
        }

        // Override TakeDamage to check for victory condition based on questions asked
        public new void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            QuestionsAsked++;

            // Boss is defeated if health reaches 0 or the max questions limit is reached
            if (IsDefeated())
            {
                Console.WriteLine($"{Name} has been defeated!");
            }

            if (QuestionsAsked == MaxQuestions)
            {
                BossWins();
            }
        }

        public void BossWins()
        {
            // Logic for the player losing
        }

        // Override GetStatus to include max questions and questions asked
        public new string GetStatus()
        {
            return $"{base.GetStatus()} - Questions Asked: {QuestionsAsked}/{MaxQuestions}";
        }
    }
}
