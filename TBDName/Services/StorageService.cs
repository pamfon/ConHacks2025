using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TBDName.Models;

namespace TBDName.Services
{
    public class StorageService
    {
        // Method to load users from CSV
        public List<User> LoadUsers()
        {
            List<User> users = new List<User>();

            // Check if the file exists
            if (File.Exists("Data/Users.csv"))
            {
                var lines = File.ReadAllLines("Data/Users.csv");

                // Skip the header and load user data from each line
                foreach (var line in lines.Skip(1)) // Skipping the header row
                {
                    var columns = line.Split(',');

                    string userId = columns[0];
                    string name = columns[1];
                    int xp = int.Parse(columns[2]);
                    int level = int.Parse(columns[3]);

                    users.Add(new User(userId, name, xp, level));
                }
            }
            else
            {
                // If the file doesn't exist, create an empty list and return it
                // You can also create the file here if you want to initialize it
                File.WriteAllText("Data/Users.csv", "UserID,Name,XP,Level\n"); // Create the file with headers
            }

            return users;
        }


        // Method to save user data to CSV
        public void SaveUser(User user)
        {
            var line = $"{user.UserID},{user.Name},{user.XP},{user.Level}";
            File.AppendAllLines("Data/Users.csv", new[] { line });
        }

        // Method to load questions from CSV
        public List<Question> LoadQuestions()
        {
            List<Question> questions = new List<Question>();

            var lines = File.ReadAllLines("Questions.csv");
            foreach (var line in lines.Skip(1)) // Skip header
            {
                var columns = line.Split(',');
                string questionText = columns[0];
                string topic = columns[1];
                int difficulty = int.Parse(columns[2]);
                string expectedAnswer = columns[3];

                questions.Add(new Question(questionText, topic, difficulty, expectedAnswer));
            }

            return questions;
        }

        // Method to load enemies from CSV
        public List<Enemy> LoadEnemies()
        {
            List<Enemy> enemies = new List<Enemy>();

            var lines = File.ReadAllLines("Data/Enemies.csv");
            foreach (var line in lines.Skip(1)) // Skip header
            {
                var columns = line.Split(',');
                string name = columns[0];
                int maxHP = int.Parse(columns[1]);
                string type = columns[2];

                EnemyType enemyType = (type.Equals("Boss", StringComparison.OrdinalIgnoreCase)) ? EnemyType.Boss : EnemyType.Regular;
                enemies.Add(new Enemy(name, maxHP, enemyType));
            }

            return enemies;
        }

        // Method to save progress to CSV (e.g., user XP or game progress)
        public void SaveProgress(User player)
        {
            string progressLine = $"{player.Name},{player.XP},{player.Level}";
            File.AppendAllLines("Data/UserProgress.csv", new[] { progressLine });
        }

        // Loading list of countries from CSV
        public List<Country> LoadCountries()
        {
            List<Country> countries = new List<Country>();
            string filePath = "Data/Countries.csv";

            if(!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "Id,Name");
                return countries;
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(',');

                if (columns.Length >= 1)
                {
                    countries.Add(new Country
                    {
                        Id = columns[0],
                        Name = columns[1],
                    });
                }
            }

            return countries;
        }

        public List<Subdivision> LoadSubdivisions(string countryId)
        {
            var subdivisions = new List<Subdivision>();
            var filePath = "Data/Subdivisions.csv";

            if (!File.Exists(filePath))
            {
                return subdivisions;
            }

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1))
            {
                var columns = line.Split(',');

                if (columns.Length >= 2 && columns[0].Trim() == countryId)
                {
                    subdivisions.Add(new Subdivision
                    {
                        Id = columns[1],
                        Name = columns[2],
                        Type = columns[3]
                    });
                }
            }

            return subdivisions;
        }

		public List<Topic> LoadTopics()
		{
			string filePath = "Data/Topics.csv";
			List<Topic> topics = new List<Topic>();

			if (!File.Exists(filePath))
			{
				File.WriteAllText(filePath, "Id,Name" +
											"governance,Governance\n" +
											"rights,Rights\n" +
											"laws,Laws");
			}

			var lines = File.ReadAllLines(filePath);

			foreach (var line in lines.Skip(1))
			{
				var columns = line.Split(",");
				if (columns.Length >= 2)
				{
					topics.Add(new Topic
					{
						Id = columns[0],
						Name = columns[1],
					});
				}
			}

			return topics;
		}
	}
}


