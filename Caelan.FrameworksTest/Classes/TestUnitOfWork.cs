using Caelan.Frameworks.BIZ.Classes;
using Caelan.Frameworks.BIZ.Interfaces;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
	public class TestUnitOfWork : BaseUnitOfWork<TestDbContext>
	{
		public TestUnitOfWork()
		{
			Users = new UserRepository(this);
			Roles = new RoleRepository(this);
			UserRoles = new UserRoleRepository(this);
		}

		public UserRepository Users { get; set; }
		public RoleRepository Roles { get; set; }
		public UserRoleRepository UserRoles { get; set; }
	}
}
