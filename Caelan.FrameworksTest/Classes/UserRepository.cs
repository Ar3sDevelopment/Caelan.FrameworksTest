using Caelan.Frameworks.BIZ.Classes;
using Caelan.Frameworks.BIZ.Interfaces;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
	public class UserRepository : Repository<User, UserDTO>
	{
		public UserRepository(IUnitOfWork manager)
			: base(manager)
		{
		}

		public UserDTO GetUserByLogin(string login, string password)
		{
			return SingleDTO(t => t.Login == login && t.Password == password);
		}
	}
}
