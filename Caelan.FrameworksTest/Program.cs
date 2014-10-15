using System;
using System.Collections.Generic;
using System.Linq;
using Caelan.Frameworks.Common.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;
using Microsoft.FSharp.Core;

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
                var users = uow.Users.ListFull().ToList();
                users.ForEach(user => Console.WriteLine("{0}: {1} [{2}]", user.Id, user.Login, string.Join(",", user.UserRoles.Where(t => t.Role != null).Select(t => t.Role.Description))));
            }
        }

        static void Update(ref UserDTO dto)
        {
            using (var uow = new TestUnitOfWork())
            {
                dto = uow.Users.GetUserByLogin(dto.Login, dto.Password);
                dto.Password = "test2";
                uow.Users.Update(dto, dto.Id);
                Console.WriteLine(uow.SaveChanges());
            }
        }

        static void Delete(UserDTO dto)
        {
            using (var uow = new TestUnitOfWork())
            {
                foreach (var ur in dto.UserRoles)
                    uow.CRUDRepository<UserRole, UserRoleDTO>().Delete(ur, ur.Id);
                uow.Users.Delete(dto, dto.Id);
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
                        IdRole = 1
                    },
                    new UserRoleDTO
                    {
                        IdRole = 2
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
