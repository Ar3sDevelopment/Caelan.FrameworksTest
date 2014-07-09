using System;
using System.Collections.Generic;
using System.Linq;
using Caelan.Frameworks.Common.Classes;
using Caelan.FrameworksTest.Classes;

namespace Caelan.FrameworksTest
{
    class Program
    {
        static void Insert(UserDTO dto)
        {
            using (var uow = new TestUnitOfWork())
            {
                uow.Users.Insert(dto);
                Console.WriteLine(uow.SaveChanges());
            }
        }

        static void Print()
        {
            using (var uow = new TestUnitOfWork())
            {
                var users = uow.Users.List().ToList();
                users.ForEach(user => Console.WriteLine("{0}: {1} [{2}]", user.ID, user.Login, string.Join(",", user.UserRoles.Where(t => t.Role != null).Select(t => t.Role.Description))));
            }
        }

        static void Update(ref UserDTO dto)
        {
            using (var uow = new TestUnitOfWork())
            {
                dto = uow.Users.GetUserByLogin(dto.Login, dto.Password);
                dto.Password = "test2";
                uow.Users.Update(dto);
                Console.WriteLine(uow.SaveChanges());
            }
        }

        static void Delete(UserDTO dto)
        {
            using (var uow = new TestUnitOfWork())
            {
                foreach (var ur in dto.UserRoles)
                    uow.UserRoles.Delete(ur);
                uow.Users.Delete(dto);
                Console.WriteLine(uow.SaveChanges());
            }
        }

        static void Main()
        {
            Console.WriteLine("C# Version");
            BuilderConfiguration.Configure();

            var dto = new UserDTO
            {
                Login = "test",
                Password = "test",
                UserRoles = new List<UserRoleDTO>
                {
                    new UserRoleDTO
                    {
                        IDRole = 1
                    },
                    new UserRoleDTO
                    {
                        IDRole = 2
                    }
                }
            };

            Insert(dto);

            Print();

            Update(ref dto);

            Delete(dto);

            Console.ReadLine();
        }
    }
}
