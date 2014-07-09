﻿module Caelan.FrameworksTestsFS.Program

open System
open System.Reflection
open Caelan.Frameworks.Common.Classes
open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes

let insert dto = 
    use uow = new TestUnitOfWork()
    uow.Users.Insert(dto)
    uow.SaveChanges() |> printfn "%d"

let print() = 
    use uow = new TestUnitOfWork()
    uow.Users.List()
    |> List.ofSeq
    |> List.iter (fun user -> (user.ID, user.Login, System.String.Join (",",user.UserRoles |> List.ofSeq |> List.map (fun t -> t.Role.Description))) |||> printfn "%d: %s [%s]")

let update (dto : UserDTO ref) = 
    use uow = new TestUnitOfWork()
    dto := uow.Users.GetUserByLogin((!dto).Login, (!dto).Password)
    (!dto).Password <- "test2"
    uow.Users.Update(!dto)
    uow.SaveChanges() |> printfn "%d"

let delete (dto : UserDTO) = 
    use uow = new TestUnitOfWork()
    //TODO: Fix
    //dto.UserRoles
    //|> List.ofSeq
    //|> List.iter (fun ur -> GenericRepository.CreateGenericCRUDRepository<UserRole, UserRoleDTO, int>(uow).Delete(ur))
    uow.Users.Delete(dto)
    uow.SaveChanges() |> printfn "%d"

[<EntryPoint>]
let main argv = 
    printfn "F# Version"
    BuilderConfiguration.Configure()
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
