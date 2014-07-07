﻿namespace Caelan.FrameworksTestsFS.Classes

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
    member val UserRoles : seq<UserRoleDTO> = null with get, set
    interface IDTO<int> with
        
        member this.ID 
            with get () = this.ID
            and set (value) = this.ID <- value

and [<AllowNullLiteral>] RoleDTO() = 
    member val ID = 0 with get, set
    member val Description = "" with get, set
    member val UserRoles : seq<UserRoleDTO> = null with get, set
    interface IDTO<int> with
        
        member this.ID 
            with get () = this.ID
            and set (value) = this.ID <- value

and [<AllowNullLiteral>] UserRoleDTO() = 
    member val ID = 0 with get, set
    member val IDUser = 0 with get, set
    member val IDRole = 0 with get, set
    member val User : User = null with get, set
    member val Role : Role = null with get, set
    interface IDTO<int> with
        
        member this.ID 
            with get () = this.ID
            and set (value) = this.ID <- value

type UserRepository(manager) = 
    inherit BaseCRUDRepository<User, UserDTO, int>(manager)
    
    member this.GetUserByLogin(login, password) = 
        let user = 
            query { 
                for item in this.All(fun t -> t.Login = login && t.Password = password) do
                    headOrDefault
            }
        this.DTOBuilder().BuildFull(user)
    
    member this.List() = this.DTOBuilder().BuildFullList(this.All())

type UserRoleRepository(manager) = 
    class
        inherit BaseCRUDRepository<UserRole, UserRoleDTO, int>(manager)
    end

type RoleRepository(manager) = 
    class
        inherit BaseCRUDRepository<Role, RoleDTO, int>(manager)
    end

type TestUnitOfWork(uowContext) as this = 
    inherit BaseUnitOfWorkManager(uowContext)
    member val Users = UserRepository(this) with get, set
    member val Roles = RoleRepository(this) with get, set
    member val UserRoles = UserRoleRepository(this) with get, set
