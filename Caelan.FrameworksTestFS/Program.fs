module Caelan.FrameworksTestsFS.Program

open System
open System.Reflection
open AutoMapper
open Caelan.FrameworksTestsFS.Classes

[<EntryPoint>]
let main argv = 
    printfn "F# Version"
    let profileType = typeof<Profile>
    
    let profiles = 
        query { 
            for t in Assembly.GetExecutingAssembly().GetTypes() do
                where 
                    (profileType.IsAssignableFrom(t) = true && t.GetConstructor(Type.EmptyTypes) <> null 
                     && t.IsGenericType = false)
                select (Activator.CreateInstance(t) :?> Profile)
        }
        |> List.ofSeq
    Mapper.Initialize(fun a -> profiles |> List.iter (fun t -> a.AddProfile(t)))
    let uow = TestUnitOfWork(TestUnitOfWorkContext())
    let mutable dto = UserDTO(Login = "test", Password = "test")
    uow.Users.Insert(dto)
    uow.SaveChanges() |> printfn "%d"
    let users = 
        uow.Users.List()
        |> List.ofSeq
        |> List.iter (fun user -> (user.ID, user.Login, user.Roles) |||> printfn "%d: %s [%s]")
    dto <- uow.Users.GetUserByLogin(dto.Login, dto.Password)
    dto.Password <- "test2"
    uow.Users.Update(dto)
    uow.SaveChanges() |> printfn "%d"
    uow.Users.Delete(dto)
    uow.SaveChanges() |> printfn "%d"
    Console.ReadLine() |> ignore
    0
