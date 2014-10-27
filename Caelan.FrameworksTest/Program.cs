using System;
using System.Collections.Generic;
using System.Linq;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.Classes;
using Caelan.FrameworksTest.Models;

namespace Caelan.FrameworksTest
{
	class Program
	{
		static void UnitOfWorkAction(Action<UnitOfWork> uowAction)
		{
			using (var uow = new UnitOfWork<TestDbContext>())
			{
				uowAction.Invoke(uow);
			}
		}

		static T UnitOfWorkFunc<T>(Func<UnitOfWork, T> uowAction)
		{
			using (var uow = new UnitOfWork<TestDbContext>())
			{
				return uowAction.Invoke(uow);
			}
		}

		static void Insert(UserDTO dto)
		{
			UnitOfWorkAction(uow =>
			{
				uow.Repository<User, UserDTO>().Insert(dto);
				Console.WriteLine(uow.SaveChanges());
			});
		}

		static void Print()
		{
			UnitOfWorkAction(uow =>
			{
				var users = uow.Repository<User, UserDTO>().List().ToList();
				users.ForEach(user => Console.WriteLine("{0}: {1} [{2}]", user.Id, user.Login, string.Join(",", user.UserRoles.Where(t => t.Role != null).Select(t => t.Role.Description))));
			});
		}

		static UserDTO Update(UserDTO dto)
		{
			return UnitOfWorkFunc(uow =>
			{
				dto = uow.Repository<UserRepository>().GetUserByLogin(dto.Login, dto.Password);
				dto.Password = "test2";
				uow.Repository<UserRepository>().Update(dto, dto.Id);
				Console.WriteLine(uow.SaveChanges());

				return dto;
			});
		}

		static void Delete(UserDTO dto)
		{
			UnitOfWorkAction(uow =>
			{
				foreach (var ur in dto.UserRoles)
				{
					uow.Repository<UserRole, UserRoleDTO>().Delete(ur, ur.Id);
				}
				uow.Repository<User, UserDTO>().Delete(dto, dto.Id);
				Console.WriteLine(uow.SaveChanges());
			});
		}

		static void Main()
		{
			Console.WriteLine("C# Version");

			var dto = new UserDTO
			{
				Login = "test",
				Password = "test",
				UserRoles = new List<UserRoleDTO>
				{
					new UserRoleDTO { IdRole = 1 },
					new UserRoleDTO { IdRole = 2 }
				}
			};

			Insert(dto);

			Print();

			dto = Update(dto);

			Delete(dto);

			Console.ReadLine();
		}
	}
}
