using System.Collections.Generic;
using Caelan.Frameworks.BIZ.Interfaces;

namespace Caelan.FrameworksTest.Classes
{
	public class RoleDTO : IDTO<int>
	{
		public int ID { get; set; }
		public string Description { get; set; }
		public IEnumerable<UserRoleDTO> UserRoles { get; set; }
	}
}
