namespace Caelan.FrameworksTestsFS.Classes

open System.Data.Entity
open System.Linq
open Caelan.Frameworks.BIZ.Interfaces
open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models

[<AllowNullLiteral>]
type UserDTO() = 
    member val Login = "" with get, set
    member val Password = "" with get, set
    member val Id = 0 with get, set
    member val UserRoles : seq<UserRoleDTO> = null with get, set
    
and [<AllowNullLiteral>] RoleDTO() = 
    member val Id = 0 with get, set
    member val Description = "" with get, set
    member val UserRoles : seq<UserRoleDTO> = null with get, set

and [<AllowNullLiteral>] UserRoleDTO() = 
    member val Id = 0 with get, set
    member val IDUser = 0 with get, set
    member val IDRole = 0 with get, set
    member val User : User = null with get, set
    member val Role : Role = null with get, set

type UserRepository(manager) = 
    inherit BaseCRUDRepository<User, UserDTO>(manager)
    
    member this.GetUserByLogin(login, password) = 
        this.Single(fun t -> t.Login = login && t.Password = password)
    
    override this.List() = this.DTOBuilder().BuildFullList(this.All())

type TestUnitOfWork() as this = 
    inherit BaseUnitOfWork<TestDbContext>()
    member val Users = UserRepository(this) with get, set