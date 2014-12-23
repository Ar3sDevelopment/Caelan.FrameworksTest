using System.Linq;
using Caelan.Frameworks.Common.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.EntityMappers
{
	public class UserEntityMapper : DefaultMapper<UserDTO, User>
	{
		public override void Map(UserDTO source, ref User destination)
		{
			destination.Id = source.Id;
			destination.Login = source.Login;
			destination.Password = source.Password;

			if (source.UserRoles != null)
			{
				destination.UserRoles = source.UserRoles.Select(t => new UserRole
				{
					Id = t.Id,
					IdUser = t.IdUser,
					IdRole = t.IdRole
				}).ToList();
			}
		}
	}
}
