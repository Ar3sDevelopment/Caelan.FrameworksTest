using System;
using Caelan.FrameworksTest.Classes;

namespace Caelan.FrameworksTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var uowContext = new TestUnitOfWorkContext();
            var uow = new TestUnitOfWork(uowContext);

            var dto = new UserDTO
            {
                Login = "test",
                Password = "test"
            };


            uow.Users.InsertAsync(dto);
            Console.WriteLine(uow.SaveChanges());

            var users = uow.Users.All();

            foreach (var user in users)
            {
                Console.WriteLine("{0}: {1}", user.ID, user.Login);
            }

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
