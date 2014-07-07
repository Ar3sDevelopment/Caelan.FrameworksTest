module Caelan.FrameworksTestsFS.Program

open System
open System.Reflection
open AutoMapper
open Caelan.FrameworksTestsFS.Classes

let insert dto = 
    use uow = new TestUnitOfWork(TestUnitOfWorkContext())
    uow.Users.Insert(dto)
    uow.SaveChanges() |> printfn "%d"

let print = 
    use uow = new TestUnitOfWork(TestUnitOfWorkContext())
    uow.Users.List()
    |> List.ofSeq
    |> List.iter (fun user -> (user.ID, user.Login, user.Roles) |||> printfn "%d: %s [%s]")

let update (dto : UserDTO ref) = 
    use uow = new TestUnitOfWork(TestUnitOfWorkContext())
    dto := uow.Users.GetUserByLogin((!dto).Login, (!dto).Password)
    (!dto).Password <- "test2"
    uow.Users.Update(!dto)
    uow.SaveChanges() |> printfn "%d"

let delete (dto : UserDTO) = 
    use uow = new TestUnitOfWork(TestUnitOfWorkContext())
    dto.UserRoles
    |> List.ofSeq
    |> List.iter (fun ur -> (uow.UserRoles.Delete(ur)))
    uow.Users.Delete(dto)
    uow.SaveChanges() |> printfn "%d"

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
    let dto = 
        ref (UserDTO(Login = "test", Password = "test", 
                     UserRoles = [ UserRoleDTO(IDRole = 1)
                                   UserRoleDTO(IDRole = 2) ]))
    insert !dto
    print
    update dto
    delete !dto
    Console.ReadLine() |> ignore
    0
