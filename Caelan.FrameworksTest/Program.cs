using System;
using Caelan.FrameworksTest.Classes;

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
            Console.WriteLine(uow.SaveChanges());

            dto = uow.Users.GetUserByLogin(dto.Login, dto.Password);

            dto.Password = "test2";

            uow.Users.Update(dto);
            Console.WriteLine(uow.SaveChanges());

            uow.Users.Delete(dto);
            Console.WriteLine(uow.SaveChanges());
            Console.ReadLine();
        }
    }
}
