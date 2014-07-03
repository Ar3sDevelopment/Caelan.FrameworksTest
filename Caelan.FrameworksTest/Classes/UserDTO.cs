using Caelan.Frameworks.BIZ.Interfaces;

namespace Caelan.FrameworksTest.Classes
{
	public class UserDTO : IDTO<int>
	{
		public int ID { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string Roles { get; set; }
	}
}
