namespace Philocivil.Models
{
    /// Represents a subdivision within a country (e.g., province, state, or territory).
    /// Used for tracking user progress at the regional level.
    public class Subdivision
    {
        /// Unique identifier for the subdivision (e.g., "ON" for Ontario, "TX" for Texas).
        /// Used for lookups in the database or CSV files.
        public string Id { get; set; }

        /// The full name of the subdivision (e.g., "Ontario", "Texas").
        /// Displayed in the UI.
        public string Name { get; set; }

        /// The type of subdivision (e.g., "Province", "State", "Territory").
        /// Helps categorize regions within different governmental structures.
        public string Type { get; set; }
    }

}