using Caelan.Frameworks.DAL.Interfaces;

namespace Caelan.FrameworksTest.Models
{
	public class UserRole : IEntity<int>
	{
		public int ID { get; set; }
		public int IDUser { get; set; }
		public int IDRole { get; set; }
		public virtual Role Role { get; set; }
		public virtual User User { get; set; }
	}
}
