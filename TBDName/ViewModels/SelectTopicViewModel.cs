using TBDName.Models;

namespace TBDName.ViewModels
{
	public class SelectTopicViewModel
	{
		public Country Country { get; set; }
		public Subdivision? Subdivision { get; set; }
		public List<Topic> Topics { get; set; }
	}
}
