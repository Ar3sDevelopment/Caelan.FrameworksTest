using System.Collections.Generic;
using System.Linq;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
	public class UserRepository : BaseCRUDRepository<User, UserDTO, int>
	{
		public UserRepository(BaseUnitOfWorkManager manager)
			: base(manager)
		{
			DbSetFunc = context =>
			{
				var customContext = context as TestDbContext;

				return customContext != null ? customContext.Users : null;
			};
		}

		public UserDTO GetUserByLogin(string login, string password)
		{
			return DTOBuilder().BuildFull(All(t => t.Login == login && t.Password == password).FirstOrDefault());
		}

		internal IEnumerable<UserDTO> List()
		{
			return DTOBuilder().BuildFullList(All());
		}
	}
}
