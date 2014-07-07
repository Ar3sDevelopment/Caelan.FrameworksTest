namespace Caelan.FrameworksTestsFS.EntityBuilders

open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes
open System.Linq

type UserEntityBuilder() = 
    inherit BaseEntityBuilder<UserDTO, User>()
    override this.AfterBuild(source, destination) = 
        base.AfterBuild(source, destination)
        if source.UserRoles <> null then 
            (!destination).UserRoles <- GenericBusinessBuilder.GenericEntityBuilder<UserRoleDTO, UserRole>()
                .BuildList(source.UserRoles).ToList()
namespace Caelan.FrameworksTestsFS.DTOBuilders

open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes

type UserDTOBuilder() = 
    inherit BaseDTOBuilder<User, UserDTO>()
    override this.BuildFull(source, destination) = 
        base.BuildFull(source, destination)
        if source.UserRoles <> null then 
            (!destination).UserRoles <- GenericBusinessBuilder.GenericDTOBuilder<UserRole, UserRoleDTO>()
                .BuildList(source.UserRoles)
