using System.Collections.Generic;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.Frameworks.Common.Extenders;
using Caelan.FrameworksTest.Models;
using Microsoft.FSharp.Core;

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
			return Single(FSharpOption<FSharpFunc<User, bool>>.Some(FunctionConverter.CreateFSharpFunc<User, bool>(t => t.Login == login && t.Password == password)));
		}

		public IEnumerable<UserDTO> ListFull()
		{
			var users = All();

			return DTOBuilder().BuildFullList(users);
		}
	}
}
