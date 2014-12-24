using System;
using System.Collections.Generic;
using System.Linq;
using Caelan.Frameworks.BIZ.Classes;
using Caelan.FrameworksTest.DTO;
using Caelan.FrameworksTest.Models;
using Caelan.FrameworksTest.Repositories;

namespace Caelan.FrameworksTest
{
	class Program
	{
		private static readonly UnitOfWorkCaller<TestDbContext> UowCaller = UnitOfWorkCaller.Context<TestDbContext>();

		static void Insert(UserDTO dto)
		{
			var res = UowCaller.UnitOfWorkCallSaveChanges(uow =>
			{
				uow.Repository<User, UserDTO>().Insert(dto);
			});
			Console.WriteLine(res ? "Insert ok" : "Insert failed");
		}

		static void Print()
		{
			UowCaller.UnitOfWork(uow =>
			{
				uow.Repository<User, UserDTO>().List().ToList().ForEach(user => Console.WriteLine("{0}: {1} [{2}]", user.Id, user.Login, string.Join(",", user.UserRoles.Where(t => t.Role != null).Select(t => t.Role.Description))));
			});
		}

		static UserDTO Update(UserDTO dto)
		{
			var res = UowCaller.UnitOfWorkCallSaveChanges(uow =>
			{
				dto = uow.Repository<UserRepository>().GetUserByLogin(dto.Login, dto.Password);
				dto.Password = "test2";
				uow.Repository<UserRepository>().Update(dto, dto.Id);
			});

			Console.WriteLine(res ? "Update ok" : "Update failed");

			return dto;
		}

		static void Delete(UserDTO dto)
		{
			var res = UowCaller.TransactionSaveChanges(uow =>
			{
				foreach (var ur in dto.UserRoles)
				{
					uow.Repository<UserRole, UserRoleDTO>().Delete(ur, ur.Id);
				}
				uow.Repository<User, UserDTO>().Delete(dto, dto.Id);
			});

			Console.WriteLine(res ? "Update ok" : "Update failed");
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
