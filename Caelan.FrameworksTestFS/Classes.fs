namespace Caelan.FrameworksTestsFS.Classes

open System.Data.Entity
open System.Linq
open Caelan.Frameworks.BIZ.Interfaces
open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models

type TestUnitOfWorkContext() = 
    let context = new TestDbContext()
    member private this.DbContext = context
    interface IUnitOfWork with
        member this.Context() = this.DbContext :> DbContext

[<AllowNullLiteral>]
type UserDTO() = 
    member val Login = "" with get, set
    member val Password = "" with get, set
    member val ID = 0 with get, set
    member val Roles = "" with get, set
    interface IDTO<int> with
        
        member this.ID 
            with get () = this.ID
            and set (value) = this.ID <- value

type UserRepository(manager) as this = 
    inherit BaseCRUDRepository<User, UserDTO, int>(manager)
    
    do 
        this.DbSetFunc <- fun t -> 
            let ctx = t :?> TestDbContext
            ctx.Users
    
    member this.GetUserByLogin(login, password) = 
        let user = 
            query { 
                for item in this.All(fun t -> t.Login = login && t.Password = password) do
                    headOrDefault
            }
        this.DTOBuilder().Build(user)

    member this.List() =
        this.DTOBuilder().BuildList(this.All())

type TestUnitOfWork(uowContext) as this = 
    inherit BaseUnitOfWorkManager(uowContext)
    member val Users = UserRepository(this) with get, set
