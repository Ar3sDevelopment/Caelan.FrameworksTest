namespace Caelan.FrameworksTest.Classes
{
	public class UserRoleDTO
	{
		public int ID { get; set; }
		public int IDUser { get; set; }
		public int IDRole { get; set; }
		public RoleDTO Role { get; set; }
		public UserDTO User { get; set; }
	}
}
