using System.Collections.Generic;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
	public class UserRepository : BaseCRUDRepository<User, UserDTO>
	{
		public UserRepository(BaseUnitOfWork manager)
			: base(manager)
		{
		}

		public UserDTO GetUserByLogin(string login, string password)
		{
			return Single(t => t.Login == login && t.Password == password);
		}

		public override IEnumerable<UserDTO> List()
		{
			var users = All();

			return DTOBuilder().BuildFullList(users);
		}
	}
}
