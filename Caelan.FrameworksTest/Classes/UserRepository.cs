using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.Frameworks.BIZ.Interfaces;
using Caelan.Frameworks.DAL.Interfaces;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest.Classes
{
    public class UserRepository : BaseCRUDRepository<User, UserDTO, int>
    {
        public UserRepository(BaseUnitOfWorkManager manager)
            : base(manager)
        {
            DbSetFunc = context =>
            {
                var customContext = context as TestDbContext;

                return customContext != null ? customContext.Users : null;
            };
        }

        public UserDTO GetUserByLogin(string login, string password)
        {
            return DTOBuilder().Build(All(t => t.Login == login && t.Password == password).FirstOrDefault());
        }
    }
}
