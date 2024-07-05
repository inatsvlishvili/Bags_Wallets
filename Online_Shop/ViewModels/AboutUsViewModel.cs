using System.ComponentModel.DataAnnotations;

namespace Bags_Wallets.ViewModels
{
	public class AboutUsViewModel
	{
		[Key]
		public int Id { get; set; }
		public string OurStoryTitle { get; set; }
		public string StoryText1 { get; set; }
		public string StoryText2 { get; set; }
		public string StoryText3 { get; set; }
		public string OurGoalTitle { get; set; }
		public string GoalText1 { get; set; }
		public string GoalText2 { get; set; }
		public string GoalText3 { get; set; }
	}
}
