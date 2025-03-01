namespace TBDName.Models
{
    public class Enemy
    {
        // Properties for the enemy details
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public EnemyType Type { get; set; } // Regular or Boss

        // Constructor to initialize the enemy
        public Enemy(string name, int maxHP, EnemyType type)
        {
            Name = name;
            MaxHP = maxHP;
            CurrentHP = maxHP; // Initially, current HP equals max HP
            Type = type;
        }

        // Method to take damage from the player's attack
        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;

            if (CurrentHP < 0)
            {
                CurrentHP = 0; // Ensure HP doesn't go below 0
            }
        }

        // Method to check if the enemy is defeated
        public bool IsDefeated()
        {
            return CurrentHP == 0;
        }

        // Method to get the enemy's current status
        public string GetStatus()
        {
            return $"{Name} - HP: {CurrentHP}/{MaxHP} ({Type})";
        }
    }

    // Enum for enemy types (Regular or Boss)
    public enum EnemyType
    {
        Regular,
        Boss
    }
}
