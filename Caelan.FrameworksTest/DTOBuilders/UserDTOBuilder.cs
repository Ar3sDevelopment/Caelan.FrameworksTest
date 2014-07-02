using System;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;
using Microsoft.FSharp.Core;

namespace Caelan.FrameworksTest.DTOBuilders
{
	public class UserDTOBuilder : BaseDTOBuilder<User, UserDTO>
	{
		public override void AfterBuild(User source, FSharpRef<UserDTO> destination)
		{
			base.AfterBuild(source, destination);

			Console.WriteLine("After");
		}

		public override UserDTO BuildFull(User source)
		{
			Console.WriteLine("Eccomi");

			return base.BuildFull(source);
		}
	}
}
