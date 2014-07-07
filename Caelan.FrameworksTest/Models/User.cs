using System.Collections.Generic;
using Caelan.Frameworks.DAL.Interfaces;

namespace Caelan.FrameworksTest.Models
{
    public class User : IEntity<int>
    {
        public User()
        {
            UserRoles = new List<UserRole>();
        }

        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
