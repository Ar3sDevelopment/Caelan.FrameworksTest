using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caelan.Frameworks.BIZ.Interfaces;

namespace Caelan.FrameworksTest.Classes
{
    public class UserDTO : IDTO<int>
    {
        public int ID { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
