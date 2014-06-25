using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.Frameworks.DAL.Interfaces;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
    public class TestUnitOfWork : BaseUnitOfWork
    {
        private readonly TestDbContext _context;

        public override DbContext Context()
        {
            return _context;
        }

        public TestUnitOfWork()
        {
            _context = new TestDbContext();

            Users = new UserRepository(this);
        }

        public UserRepository Users { get; set; }
    }
}
