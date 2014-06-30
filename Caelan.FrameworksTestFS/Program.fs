module Caelan.FrameworksTestsFS.Program

open Caelan.FrameworksTestsFS.Classes
open Caelan.FrameworksTestsFS.Models

[<EntryPoint>]
let main argv = 
    let uowContext = TestUnitOfWorkContext()
    let uow = TestUnitOfWork(uowContext)
    let mutable dto = UserDTO()

    dto.Login <- "test"
    dto.Password <- "test"

    uow.Users.Insert(dto)
    uow.SaveChanges() |> printfn "%d"

    let users = uow.Users.All()

    for user in users do
        printfn "%d: %s" user.Id user.Login

    dto <- uow.Users.GetUserByLogin(dto.Login, dto.Password)

    dto.Password <- "test2"

    uow.Users.Update(dto)
    uow.SaveChanges() |> printfn "%d"

    uow.Users.Delete(dto)
    uow.SaveChanges() |> printfn "%d"

    System.Console.ReadLine() |> ignore

    0
