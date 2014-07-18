using System.Collections.Generic;

namespace Caelan.FrameworksTest.Classes
{
	public class RoleDTO
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public IEnumerable<UserRoleDTO> UserRoles { get; set; }
	}
}
