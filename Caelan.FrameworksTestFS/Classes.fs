namespace Caelan.FrameworksTestsFS.Classes

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
    inherit Repository<User, UserDTO>(manager)
    
    member this.GetUserByLogin(login, password) = 
        this.Single(fun (t : User) -> t.Login = login && t.Password = password)
    
    member this.ListFull() = this.DTOBuilder().BuildFullList(this.All())