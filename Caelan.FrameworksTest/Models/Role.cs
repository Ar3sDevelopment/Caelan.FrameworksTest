using System.Collections.Generic;

namespace Caelan.FrameworksTest.Models
{
    public class Role
    {
        public Role()
        {
            UserRoles = new List<UserRole>();
        }

        public int ID { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
