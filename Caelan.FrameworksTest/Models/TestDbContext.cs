using System.Data.Entity;
using Caelan.Frameworks.DAL.Interfaces;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models.Mapping;

namespace Caelan.FrameworksTest.Models
{
    public class TestDbContext : DbContext
    {
        static TestDbContext()
        {
            Database.SetInitializer<TestDbContext>(null);
        }

        public TestDbContext()
            : base("Name=TestDbContext")
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserMap());
        }

        public int Save()
        {
            return SaveChanges();
        }
    }
}
