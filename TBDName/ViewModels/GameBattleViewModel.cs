using TBDName.Models;

namespace TBDName.ViewModels
{
	public class GameBattleViewModel
	{
		public User Player { get; set; }
		public Enemy Enemy { get; set; }
		public Country Country { get; set; }
		public Subdivision Subdivision { get; set; }
		public string CurrentQuestion { get; set; }
		public string AIResponse { get; set; }
		public string UserAnswer { get; set; }
		public int Score { get; set; }
	}
}
