using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
	public class RoleRepository : BaseCRUDRepository<Role, RoleDTO, int>
	{
		public RoleRepository(BaseUnitOfWorkManager manager)
			: base(manager)
		{
		}
	}
}
