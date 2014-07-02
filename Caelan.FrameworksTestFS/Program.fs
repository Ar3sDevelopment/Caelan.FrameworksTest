module Caelan.FrameworksTestsFS.Program

open System
open System.Linq
open System.Reflection
open AutoMapper
open Caelan.FrameworksTestsFS.Classes
open Caelan.FrameworksTestsFS.Models

[<EntryPoint>]
let main argv = 
    let profileType = typeof<Profile>;
    let profiles = Assembly.GetExecutingAssembly().GetTypes().Where(fun t -> profileType.IsAssignableFrom(t) = true && t.GetConstructor(Type.EmptyTypes) <> null && t.IsGenericType = false).Select(fun t -> Activator.CreateInstance(t)).Cast<Profile>().ToList();

    Mapper.Initialize(fun a -> profiles.ForEach(fun t -> a.AddProfile(t)));
    let uowContext = TestUnitOfWorkContext()
    let uow = TestUnitOfWork(uowContext)
    let mutable dto = UserDTO()
    dto.Login <- "test"
    dto.Password <- "test"
    uow.Users.Insert(dto)
    uow.SaveChanges() |> printfn "%d"
    let users = uow.Users.All()
    for user in users do
        printfn "%d: %s" user.ID user.Login
    dto <- uow.Users.GetUserByLogin(dto.Login, dto.Password)
    dto.Password <- "test2"
    uow.Users.Update(dto)
    uow.SaveChanges() |> printfn "%d"
    uow.Users.Delete(dto)
    uow.SaveChanges() |> printfn "%d"
    System.Console.ReadLine() |> ignore
    0
