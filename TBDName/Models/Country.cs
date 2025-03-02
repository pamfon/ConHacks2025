namespace Philocivil.Models
{

    /// Represents a country in the system.
    /// Used for tracking user progress and game structure.
    public class Country
    {

        /// Unique identifier for the country (e.g., "canada", "usa").
        /// This ID is used for lookups in the database or CSV files.
        public string Id { get; set; }

        /// The full name of the country (e.g., "Canada", "United States").
        /// This name is displayed in the UI.
        public string Name { get; set; }
    }

}
