using System.Linq;
using Caelan.Frameworks.Common.Interfaces;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;
using Microsoft.FSharp.Core;

namespace Caelan.FrameworksTest.EntityMappers
{
	public class UserEntityMapper : IMapper<UserDTO, User>
	{
		public User Map(UserDTO source)
		{
			var dest = new User();
			var destRef = new FSharpRef<User>(dest);

			Map(source, destRef);

			return destRef.Value;
		}

		public void Map(UserDTO source, FSharpRef<User> destination)
		{
			destination.Value.Id = source.Id;
			destination.Value.Login = source.Login;
			destination.Value.Password = source.Password;

			if (source.UserRoles != null)
			{
				destination.Value.UserRoles = source.UserRoles.Select(t => new UserRole
				{
					IdRole = t.IdRole
				}).ToList();
			}
		}
	}
}
