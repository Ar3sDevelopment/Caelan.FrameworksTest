namespace Caelan.FrameworksTestsFS.EntityMappers

open Caelan.Frameworks.Common.Interfaces
open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes
open System.Linq

type UserEntityMapper() = 
    interface IMapper<UserDTO, User> with
        member this.Map(source : UserDTO) = 
            let dest = User()
            let destRef = ref dest
            this.Map(source, destRef)
            !destRef
        member this.Map(source : UserDTO, destination : User ref) =
            destination := User(Id = source.Id, Login = source.Login, Password = source.Password)
            (!destination).UserRoles <- (source.UserRoles |> Seq.map (fun t -> UserRole(IDRole = t.IDRole))).ToList()

    member this.Map(source : UserDTO) = (this :> IMapper<UserDTO, User>).Map(source)
    member this.Map(source : UserDTO, destination : User ref) = (this :> IMapper<UserDTO, User>).Map(source, destination)

namespace Caelan.FrameworksTestsFS.DTOMappers

open Caelan.Frameworks.Common.Interfaces
open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes

type UserDTOMapper() = 
    interface IMapper<User, UserDTO> with
        member this.Map(source : User) =
            let dest = UserDTO()
            let destRef = ref dest
            this.Map(source, destRef)
            !destRef
        member this.Map(source : User, destination : UserDTO ref) =
            destination := UserDTO(Id = source.Id, Login = source.Login, Password = source.Password)
            (!destination).UserRoles <- source.UserRoles |> Seq.map (fun t -> UserRoleDTO(Id = t.Id, IDUser = t.IDUser, IDRole = t.IDRole, Role = RoleDTO(Id = t.Role.Id, Description = t.Role.Description)))

    member this.Map(source : User) = (this :> IMapper<User, UserDTO>).Map(source)
    member this.Map(source : User, destination : UserDTO ref) = (this :> IMapper<User, UserDTO>).Map(source, destination)