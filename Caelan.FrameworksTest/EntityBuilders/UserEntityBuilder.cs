using System.Linq;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.EntityBuilders
{
	public class UserEntityBuilder : BaseEntityBuilder<UserDTO, User>
	{
		public override User AfterBuild(UserDTO source, User destination)
		{
			destination = base.AfterBuild(source, destination);

			destination.UserRoles = GenericBusinessBuilder.GenericEntityBuilder<UserRoleDTO, UserRole>().BuildList(source.UserRoles).ToList();

			return destination;
		}
	}
}
