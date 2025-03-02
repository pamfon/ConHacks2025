using TBDName.Models;

namespace TBDName.ViewModels
{
	public class GameViewModel
	{
		public int Level { get; set; }
		public string Country { get; set; }
		public string Subdivision { get; set; }
		public string Topic { get; set; }
		public string Answer { get; set; }
		public GameSession GameSession { get; set; }
	}
}