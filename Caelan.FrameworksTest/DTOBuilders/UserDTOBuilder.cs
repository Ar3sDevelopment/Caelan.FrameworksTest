﻿using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;
using Microsoft.FSharp.Core;

namespace Caelan.FrameworksTest.DTOBuilders
{
	public class UserDTOBuilder : BaseDTOBuilder<User, UserDTO>
	{
		public override void BuildFull(User source, FSharpRef<UserDTO> destination)
		{
			base.BuildFull(source, destination);

			if (source.UserRoles != null)
				destination.Value.UserRoles = GenericBusinessBuilder.GenericDTOBuilder<UserRole, UserRoleDTO>().BuildList(source.UserRoles);
		}
	}
}
