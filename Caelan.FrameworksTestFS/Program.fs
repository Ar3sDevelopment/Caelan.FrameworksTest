module Caelan.FrameworksTestsFS.Program

open System
open Caelan.Frameworks.Common.Classes
open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes

let insert (dto : UserDTO) = 
    use uow = new UnitOfWork<TestDbContext>()
    uow.Repository<User, UserDTO>().Insert(dto)
    uow.SaveChanges() |> printfn "%d"

let print() = 
    use uow = new UnitOfWork<TestDbContext>()
    uow.Repository<User, UserDTO>().List()
    |> List.ofSeq
    |> List.iter (fun user -> 
           (user.Id, user.Login, 
            System.String.Join(",", 
                               user.UserRoles
                               |> List.ofSeq
                               |> List.map (fun t -> t.Role.Description)))
           |||> printfn "%d: %s [%s]")

let update (dto : UserDTO ref) = 
    use uow = new UnitOfWork<TestDbContext>()
    dto := uow.Repository<UserRepository>().GetUserByLogin((!dto).Login, (!dto).Password)
    (!dto).Password <- "test2"
    uow.Repository<User, UserDTO>().Update(!dto, [| box (!dto).Id |])
    uow.SaveChanges() |> printfn "%d"

let delete (dto : UserDTO) = 
    use uow = new UnitOfWork<TestDbContext>()
    dto.UserRoles
    |> List.ofSeq
    |> List.iter (fun ur -> uow.Repository<UserRole, UserRoleDTO>().Delete(ur, [| box ur.Id |]))
    uow.Repository<User, UserDTO>().Delete(dto, [| box dto.Id |])
    uow.SaveChanges() |> printfn "%d"

[<EntryPoint>]
let main _ = 
    printfn "F# Version"
    let dto = 
        ref (UserDTO(Login = "test", Password = "test", 
                     UserRoles = [ UserRoleDTO(IDRole = 1)
                                   UserRoleDTO(IDRole = 2) ]))
    insert !dto
    print()
    update dto
    delete !dto
    Console.ReadLine() |> ignore
    0
