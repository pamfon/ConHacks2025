namespace Philocivil.Models
{
    public class Enemy
    {
        // Properties for the enemy details
        public string Name { get; set; }
        // public int MaxHP { get; set; }
        public int HP { get; set; }
        public EnemyType Type { get; set; } // Regular or Boss


        public Enemy(string name, int hp, EnemyType type)
        {
            Name = name;
            HP = hp;
            Type = type;
        }

        // Method to take damage from the player's attack
        public void TakeDamage(int damage)
        {
            HP -= damage;

            if (HP < 0)
            {
                HP = 0; // Ensure HP doesn't go below 0
            }
        }

        // Method to check if the enemy is defeated
        public bool IsDefeated()
        {
            return HP <= 0;
        }

        // Method to get the enemy's current status
        public string GetStatus()
        {
            return $"{Name} - HP: {HP})";
        }
    }

    // Enum for enemy types (Regular or Boss)
    public enum EnemyType
    {
        Regular,
        Boss
    }
}
