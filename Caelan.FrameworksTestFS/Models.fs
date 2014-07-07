namespace Caelan.FrameworksTestsFS.Models

open System
open System.Collections.Generic
open System.ComponentModel.DataAnnotations.Schema
open System.Data.Entity
open System.Data.Entity.ModelConfiguration
open Caelan.Frameworks.DAL.Interfaces

[<AllowNullLiteral>]
type User() as self = 
    [<DefaultValue>] val mutable userRoles : ICollection<UserRole>
    
    [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>] member val ID = 0 with get, set
    
    interface IEntity<int> with
        
        member this.ID 
            with get () = self.ID
            and set (value) = self.ID <- value
    
    member val Login = "" with get, set
    member val Password = "" with get, set
    abstract UserRoles : ICollection<UserRole> with get, set
    
    override this.UserRoles 
        with get () = this.userRoles
        and set (v : ICollection<UserRole>) = this.userRoles <- v

and [<AllowNullLiteral>] UserRole() as self = 
    [<DefaultValue>] val mutable user : User
    [<DefaultValue>] val mutable role : Role
    
    [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>] member val ID = 0 with get, set
    
    interface IEntity<int> with
        
        member this.ID 
            with get () = self.ID
            and set (value) = self.ID <- value
    
    member val IDUser = 0 with get, set
    member val IDRole = 0 with get, set
    abstract User : User with get, set
    
    override this.User 
        with get () = this.user
        and set (v : User) = this.user <- v
    
    abstract Role : Role with get, set
    
    override this.Role 
        with get () = this.role
        and set (v : Role) = this.role <- v

and [<AllowNullLiteral>] Role() as self = 
    [<DefaultValue>] val mutable userRoles : ICollection<UserRole>
    
    [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>] member val ID = 0 with get, set
    
    interface IEntity<int> with
        
        member this.ID 
            with get () = self.ID
            and set (value) = self.ID <- value
    
    member val Description = "" with get, set
    abstract UserRoles : ICollection<UserRole> with get, set
    
    override this.UserRoles 
        with get () = this.userRoles
        and set (v : ICollection<UserRole>) = this.userRoles <- v

type UserMap() as this = 
    inherit EntityTypeConfiguration<User>()
    do 
        this.HasKey(fun t -> t.ID) |> ignore
        this.Property(fun t -> t.Login).IsRequired().HasMaxLength(new Nullable<int>(50)) |> ignore
        this.Property(fun t -> t.Password).IsRequired().HasMaxLength(new Nullable<int>(50)) |> ignore
        this.ToTable("User") |> ignore
        this.Property(fun t -> (t :> IEntity<int>).ID).HasColumnName("Id") |> ignore
        this.Property(fun t -> t.Login).HasColumnName("Login") |> ignore
        this.Property(fun t -> t.Password).HasColumnName("Password") |> ignore

type RoleMap() as this = 
    inherit EntityTypeConfiguration<Role>()
    do 
        this.HasKey(fun t -> t.ID) |> ignore
        this.Property(fun t -> t.Description).IsRequired().HasMaxLength(new Nullable<int>(50)) |> ignore
        this.ToTable("Role") |> ignore
        this.Property(fun t -> (t :> IEntity<int>).ID).HasColumnName("Id") |> ignore
        this.Property(fun t -> t.Description).HasColumnName("Description") |> ignore

type UserRoleMap() as this = 
    inherit EntityTypeConfiguration<UserRole>()
    do 
        this.HasKey(fun t -> t.ID) |> ignore
        this.ToTable("UserRole") |> ignore
        this.Property(fun t -> (t :> IEntity<int>).ID).HasColumnName("Id") |> ignore
        this.Property(fun t -> t.IDUser).HasColumnName("IdUser") |> ignore
        this.Property(fun t -> t.IDRole).HasColumnName("IdRole") |> ignore
        this.HasRequired(fun t -> t.Role).WithMany(fun t -> t.UserRoles).HasForeignKey(fun d -> d.IDRole) |> ignore
        this.HasRequired(fun t -> t.User).WithMany(fun t -> t.UserRoles).HasForeignKey(fun d -> d.IDUser) |> ignore

type TestDbContext() = 
    inherit DbContext("Name=TestDbContext")
    do Database.SetInitializer<TestDbContext>(null)
    [<DefaultValue>] val mutable users : DbSet<User>
    [<DefaultValue>] val mutable roles : DbSet<Role>
    [<DefaultValue>] val mutable userRoles : DbSet<UserRole>
    
    member this.Users 
        with get () = this.users
        and set value = this.users <- value
    
    member this.Roles 
        with get () = this.roles
        and set value = this.roles <- value
    
    member this.UserRoles 
        with get () = this.userRoles
        and set value = this.userRoles <- value
    
    override this.OnModelCreating(builder : DbModelBuilder) = 
        builder.Configurations.Add(UserMap()) |> ignore
        builder.Configurations.Add(RoleMap()) |> ignore
        builder.Configurations.Add(UserRoleMap()) |> ignore
