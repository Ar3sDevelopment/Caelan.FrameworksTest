using System.Linq;
using Caelan.Frameworks.Common.Interfaces;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;
using Microsoft.FSharp.Core;

namespace Caelan.FrameworksTest.EntityMappers
{
	public class UserDTOMapper : IMapper<User, UserDTO>
	{
		public UserDTO Map(User source)
		{
			var dest = new UserDTO();
			var destRef = new FSharpRef<UserDTO>(dest);

			Map(source, destRef);

			return destRef.Value;
		}

		public void Map(User source, FSharpRef<UserDTO> destination)
		{
			destination.Value.Id = source.Id;
			destination.Value.Login = source.Login;
			destination.Value.Password = source.Password;

			if (source.UserRoles != null)
			{
				destination.Value.UserRoles = source.UserRoles.Select(t => new UserRoleDTO
				{
					Id = t.Id,
					IdUser = t.IdUser,
					IdRole = t.IdRole,
					Role = new RoleDTO
					{
						Id = t.Role.Id,
						Description = t.Role.Description
					}
				});
			}
		}
	}
}
