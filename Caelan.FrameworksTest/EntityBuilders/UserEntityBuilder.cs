using System.Linq;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;
using Microsoft.FSharp.Core;

namespace Caelan.FrameworksTest.EntityBuilders
{
	public class UserEntityBuilder : BaseEntityBuilder<UserDTO, User>
	{
		public override void AfterBuild(UserDTO source, FSharpRef<User> destination)
		{
			base.AfterBuild(source, destination);

			destination.Value.UserRoles = GenericBusinessBuilder.GenericEntityBuilder<UserRoleDTO, UserRole>().BuildList(source.UserRoles).ToList();
		}
	}
}
