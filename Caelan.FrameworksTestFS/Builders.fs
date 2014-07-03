namespace Caelan.FrameworksTestsFS.EntityBuilders

open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes

type UserEntityBuilder() =
    class
    inherit BaseEntityBuilder<UserDTO, User>()
    end

namespace Caelan.FrameworksTestsFS.DTOBuilders

open Caelan.Frameworks.BIZ.Classes
open Caelan.FrameworksTestsFS.Models
open Caelan.FrameworksTestsFS.Classes

type UserDTOBuilder() = 
    inherit BaseDTOBuilder<User, UserDTO>()
    override this.AfterBuild(source, destination) = 
        base.AfterBuild(source, destination)
        (!destination).Roles <- "Pippo"
