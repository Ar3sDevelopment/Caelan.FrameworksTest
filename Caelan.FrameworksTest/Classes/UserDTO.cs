using System.Collections.Generic;

namespace Caelan.FrameworksTest.Classes
{
	public class UserDTO
	{
		public int ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public virtual IEnumerable<UserRoleDTO> UserRoles { get; set; }
	}
}
