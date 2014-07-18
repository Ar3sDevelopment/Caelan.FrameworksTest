using System.Collections.Generic;

namespace Caelan.FrameworksTest.Models
{
    public class User
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
