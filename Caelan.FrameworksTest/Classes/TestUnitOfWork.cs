using Caelan.Frameworks.BIZ.Classes;
using Caelan.Frameworks.BIZ.Interfaces;

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
