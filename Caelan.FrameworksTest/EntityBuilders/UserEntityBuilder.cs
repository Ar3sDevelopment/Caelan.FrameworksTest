using System.Linq;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.EntityBuilders
{
    public class UserEntityBuilder : BaseEntityBuilder<UserDTO, User>
    {
        public override void AfterBuild(UserDTO source, ref User destination)
        {
            base.AfterBuild(source, ref destination);

            destination.UserRoles = GenericBusinessBuilder.GenericEntityBuilder<UserRoleDTO, UserRole>().BuildList(source.UserRoles).ToList();
        }
    }
}
