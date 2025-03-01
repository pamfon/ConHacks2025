namespace TBDName.Models
{
    public class User
    {
        // Properties for user data

        public string UserID { get; set; }
        public string Name { get; set; }
        public int XP { get; set; }
        public int Level { get; set; }

        private const int BaseXPRequirement = 10;

        // Constructor to initialize the user with default values
        public User(string userId)
        {
            UserID = userId;
            Name = $"Player{userId}";
            XP = 0; // Default XP
            Level = 1; // Default level
        }

        public User(string userId, string name, int xp, int level)
        {
            UserID = userId;
            Name = name;
            XP = xp;
            Level = level;
        }

        // Method to gain XP
        public void GainXP(int amount)
        {
            XP += amount;

            while (XP >= GetXPRequirementForNextLevel())
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            XP -= GetXPRequirementForNextLevel();
            Level++;
        }

        private int GetXPRequirementForNextLevel()
        {
            return (int)Math.Ceiling(BaseXPRequirement * Math.Pow(1.2, Level - 1));
        }

        // Method to represent the user's current status
        public string GetStatus()
        {
            return $"{Name} - Level: {Level}, XP: {XP}";
        }
    }
}
