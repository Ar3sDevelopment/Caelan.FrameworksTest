using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Caelan.FrameworksTest.Classes;

namespace Caelan.FrameworksTest
{
	class Program
	{
		static void Main()
		{
			Console.WriteLine("C# Version");
			var profileType = typeof(Profile);

			var profiles = (from t in Assembly.GetExecutingAssembly().GetTypes()
							where profileType.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null && !t.IsGenericType
							select (Profile)Activator.CreateInstance(t)).ToList();
			Mapper.Initialize(a => profiles.ForEach(a.AddProfile));
			var uow = new TestUnitOfWork(new TestUnitOfWorkContext());
			var dto = new UserDTO { Login = "test", Password = "test" };
			uow.Users.Insert(dto);
			Console.WriteLine(uow.SaveChanges());
			var users = uow.Users.List().ToList();
			users.ForEach(user => Console.WriteLine("{0}: {1} [{2}]", user.ID, user.Login, user.Roles));
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
