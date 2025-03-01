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

		// Loads a list of countries from the "Countries.csv" file
		public List<Country> LoadCountries()
		{
			List<Country> countries = new List<Country>();
			string filePath = "Data/Countries.csv";

			// Check if the file exists; if not, create it with a header and return an empty list
			if (!File.Exists(filePath))
			{
				File.WriteAllText(filePath, "Id,Name");
				return countries;
			}

			// Read all lines from the CSV file
			var lines = File.ReadAllLines(filePath);

			// Skip the header row and process each line
			foreach (var line in lines.Skip(1))
			{
				var columns = line.Split(',');

				// Ensure the line has at least one column (ID should always exist)
				if (columns.Length >= 2)
				{
					// Create a new Country object and add it to the list
					countries.Add(new Country
					{
						Id = columns[0],  // Country ID (e.g., "canada")
						Name = columns[1] // Country Name (e.g., "Canada")
					});
				}
			}

			return countries;
		}

		// Loads subdivisions (e.g., provinces, states, territories) from "Subdivisions.csv" for a given country
		public List<Subdivision> LoadSubdivisions(string countryId)
		{
			var subdivisions = new List<Subdivision>();
			var filePath = "Data/Subdivisions.csv";

			// If the file does not exist, return an empty list
			if (!File.Exists(filePath))
			{
				return subdivisions;
			}

			// Read all lines from the CSV file
			var lines = File.ReadAllLines(filePath);

			// Skip the header row and process each line
			foreach (var line in lines.Skip(1))
			{
				var columns = line.Split(',');

				// Ensure the line has at least two columns and matches the given country ID
				if (columns.Length >= 3 && columns[0].Trim() == countryId)
				{
					// Create a new Subdivision object and add it to the list
					subdivisions.Add(new Subdivision
					{
						Id = columns[1],   // Subdivision ID (e.g., "ON" for Ontario)
						Name = columns[2], // Subdivision Name (e.g., "Ontario")
						Type = columns[3]  // Type of subdivision (e.g., "Province", "State")
					});
				}
			}

			return subdivisions;
		}

		// Loads topics (e.g., Governance, Rights, Laws) from "Topics.csv"
		public List<Topic> LoadTopics()
		{
			string filePath = "Data/Topics.csv";
			List<Topic> topics = new List<Topic>();

			// If the file does not exist, create it with default topics
			if (!File.Exists(filePath))
			{
				File.WriteAllText(filePath,
					"Id,Name\n" +
					"governance,Governance\n" +
					"rights,Rights\n" +
					"laws,Laws"
				);
			}

			// Read all lines from the CSV file
			var lines = File.ReadAllLines(filePath);

			// Skip the header row and process each line
			foreach (var line in lines.Skip(1))
			{
				var columns = line.Split(",");

				// Ensure the line has at least two columns
				if (columns.Length >= 2)
				{
					// Create a new Topic object and add it to the list
					topics.Add(new Topic
					{
						Id = columns[0],  // Topic ID (e.g., "governance")
						Name = columns[1] // Topic Name (e.g., "Governance")
					});
				}
			}

			return topics;
		}
	}
}


