using System.Data.Entity;
using Caelan.Frameworks.BIZ.Interfaces;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
    public class TestUnitOfWorkContext : IUnitOfWork
    {
        public DbContext Context()
        {
            return new TestDbContext();
        }
    }
}
