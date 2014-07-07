using System.Collections.Generic;
using Caelan.Frameworks.DAL.Interfaces;

namespace Caelan.FrameworksTest.Models
{
    public class Role : IEntity<int>
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
