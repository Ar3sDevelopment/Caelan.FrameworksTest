using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Caelan.FrameworksTest.Classes;

namespace Caelan.FrameworksTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var profileType = typeof(Profile);
			var profiles = Assembly.GetExecutingAssembly().GetTypes().Where(t => profileType.IsAssignableFrom(t) && t.GetConstructor(Type.EmptyTypes) != null && !t.IsGenericType).Select(Activator.CreateInstance).Cast<Profile>().ToList();

			Mapper.Initialize(a => profiles.ForEach(a.AddProfile));

			var uowContext = new TestUnitOfWorkContext();
			var uow = new TestUnitOfWork(uowContext);

			var dto = new UserDTO
			{
				Login = "test",
				Password = "test"
			};

			Console.WriteLine(uow.Users.DTOBuilder().GetType().Name);

			uow.Users.Insert(dto);
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
