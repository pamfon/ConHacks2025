namespace TBDName.Models
{
	/// Represents a topic within a government level (e.g., Governance, Rights, Laws).
	/// Used to structure quizzes and track user progress.
	public class Topic
	{
		/// Unique identifier for the topic (e.g., "governance", "rights", "laws").
		/// Used for lookups in the database or CSV files.
		public string Id { get; set; }

		/// The full name of the topic (e.g., "Governance", "Rights", "Laws").
		/// Displayed in the UI.
		public string Name { get; set; }
	}
}
