namespace Caelan.FrameworksTest.Models
{
	public class UserRole
	{
		public int Id { get; set; }
		public int IdUser { get; set; }
		public int IdRole { get; set; }
		public virtual Role Role { get; set; }
		public virtual User User { get; set; }
	}
}
