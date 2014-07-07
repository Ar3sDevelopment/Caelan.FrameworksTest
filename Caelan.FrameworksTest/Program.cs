using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Caelan.FrameworksTest.Classes;

namespace Caelan.FrameworksTest
{
	class Program
	{
		static void Insert(UserDTO dto)
		{
			using (var uow = new TestUnitOfWork(new TestUnitOfWorkContext()))
			{
				uow.Users.Insert(dto);
				Console.WriteLine(uow.SaveChanges());
			}
		}

		static void Print()
		{
			using (var uow = new TestUnitOfWork(new TestUnitOfWorkContext()))
			{
				var users = uow.Users.List().ToList();
				users.ForEach(user => Console.WriteLine("{0}: {1} [{2}]", user.ID, user.Login, string.Join(",", user.UserRoles.Where(t => t.Role != null).Select(t => t.Role.Description))));
			}
		}

		static void Update(ref UserDTO dto)
		{
			using (var uow = new TestUnitOfWork(new TestUnitOfWorkContext()))
			{
				dto = uow.Users.GetUserByLogin(dto.Login, dto.Password);
				dto.Password = "test2";
				uow.Users.Update(dto);
				Console.WriteLine(uow.SaveChanges());
			}
		}

		static void Delete(UserDTO dto)
		{
			using (var uow = new TestUnitOfWork(new TestUnitOfWorkContext()))
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
			var profileType = typeof(Profile);

			var profiles = (from t in Assembly.GetExecutingAssembly().GetTypes()
							where profileType.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null && !t.IsGenericType
							select (Profile)Activator.CreateInstance(t)).ToList();
			Mapper.Initialize(a => profiles.ForEach(a.AddProfile));

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
