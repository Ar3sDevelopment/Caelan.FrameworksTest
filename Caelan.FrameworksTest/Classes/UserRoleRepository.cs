using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
	public class UserRoleRepository : BaseCRUDRepository<UserRole, UserRoleDTO, int>
	{
		public UserRoleRepository(BaseUnitOfWorkManager manager)
			: base(manager)
		{
		}
	}
}
