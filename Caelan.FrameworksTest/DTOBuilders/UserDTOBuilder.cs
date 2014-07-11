using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.DTOBuilders
{
    public class UserDTOBuilder : BaseDTOBuilder<User, UserDTO>
    {
        public override void BuildFull(User source, ref UserDTO destination)
        {
            base.BuildFull(source, ref destination);

            if (source.UserRoles != null)
                destination.UserRoles = GenericBusinessBuilder.GenericDTOBuilder<UserRole, UserRoleDTO>().BuildList(source.UserRoles);
        }
    }
}
