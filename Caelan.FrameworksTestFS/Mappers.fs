namespace Caelan.FrameworksTestsFS.EntityMappers

open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes
open System.Linq
open Caelan.Frameworks.Common.Classes

type UserEntityMapper() = 
    inherit DefaultMapper<UserDTO, User>()
    override __.Map(source : UserDTO, destination : User byref) = 
        destination.Id <- source.Id
        destination.Login <- source.Login
        destination.Password <- source.Password
        destination.UserRoles <- (source.UserRoles |> Seq.map (fun t -> UserRole(IDRole = t.IDRole))).ToList()
namespace Caelan.FrameworksTestsFS.DTOMappers

open Caelan.Frameworks.Common.Interfaces
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes
open Caelan.Frameworks.Common.Classes

type UserDTOMapper() = 
    inherit DefaultMapper<User, UserDTO>()
    override __.Map(source : User, destination : UserDTO byref) = 
        destination.Id <- source.Id
        destination.Login <- source.Login
        destination.Password <- source.Password
        destination.UserRoles <- source.UserRoles 
                                 |> Seq.map 
                                        (fun t -> 
                                        UserRoleDTO
                                            (Id = t.Id, IDUser = t.IDUser, IDRole = t.IDRole, 
                                             Role = RoleDTO(Id = t.Role.Id, Description = t.Role.Description)))
