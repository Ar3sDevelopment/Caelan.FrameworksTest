using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var uow = new TestUnitOfWork();

            var dto = new UserDTO
            {
                Login = "test",
                Password = "test"
            };


            uow.Users.Insert(dto);
            uow.SaveChanges();

            dto = uow.Users.GetUserByLogin(dto.Login, dto.Password);

            dto.Password = "test2";

            uow.Users.Update(dto);
            uow.SaveChanges();

            uow.Users.Delete(dto);
            uow.SaveChanges();
        }
    }
}
