using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.Frameworks.BIZ.Interfaces;
using Caelan.Frameworks.DAL.Interfaces;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
    public class TestUnitOfWork : BaseUnitOfWorkManager
    {

        public TestUnitOfWork(IUnitOfWork uow)
            : base(uow)
        {
            Users = new UserRepository(this);
        }

        public UserRepository Users { get; set; }
    }
}
