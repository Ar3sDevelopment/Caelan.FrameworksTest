using System.Linq;
using Caelan.Frameworks.Common.Classes;
using Caelan.FrameworksTest.DTO;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.DTOMappers
{
	public class UserDTOMapper : DefaultMapper<User, UserDTO>
	{
		public override void Map(User source, ref UserDTO destination)
		{
			destination.Id = source.Id;
			destination.Login = source.Login;
			destination.Password = source.Password;

			if (source.UserRoles != null)
			{
				destination.UserRoles = source.UserRoles.Select(t => new UserRoleDTO
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
