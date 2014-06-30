namespace Caelan.FrameworksTestsFS.Models

open System
open System.ComponentModel.DataAnnotations.Schema
open System.Data.Entity
open System.Data.Entity.ModelConfiguration
open Caelan.Frameworks.DAL.Interfaces

[<AllowNullLiteral>]
type User() = 
    
    [<DatabaseGenerated(DatabaseGeneratedOption.Identity)>] member val Id = 0 with get, set
    
    interface IEntity<int> with
        
        [<NotMapped>]
        member this.ID
            with get () = this.Id
            and set (value) = this.Id <- value
    
    member val Login = "" with get, set
    member val Password = "" with get, set

type UserMap() as this = 
    inherit EntityTypeConfiguration<User>()
    do 
        this.HasKey(fun t -> t.Id) |> ignore
        this.Property(fun t -> t.Login).IsRequired().HasMaxLength(new Nullable<int>(50)) |> ignore
        this.Property(fun t -> t.Password).IsRequired().HasMaxLength(new Nullable<int>(50)) |> ignore
        this.ToTable("User") |> ignore
        this.Property(fun t -> t.Id).HasColumnName("Id") |> ignore
        this.Property(fun t -> t.Login).HasColumnName("Login") |> ignore
        this.Property(fun t -> t.Password).HasColumnName("Password") |> ignore

type TestDbContext() = 
    inherit DbContext("Name=TestDbContext")
    member val Users : DbSet<User> = null with get, set
    static member TestDbContext() = Database.SetInitializer<TestDbContext>(null)
    override this.OnModelCreating(builder : DbModelBuilder) = builder.Configurations.Add(UserMap()) |> ignore
