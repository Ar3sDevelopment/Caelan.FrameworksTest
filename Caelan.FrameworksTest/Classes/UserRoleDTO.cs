using Caelan.Frameworks.BIZ.Interfaces;

namespace Caelan.FrameworksTest.Classes
{
	public class UserRoleDTO : IDTO<int>
	{
		public int ID { get; set; }
		public int IDUser { get; set; }
		public int IDRole { get; set; }
		public RoleDTO Role { get; set; }
		public UserDTO User { get; set; }
	}
}
