using System.Data.Entity.ModelConfiguration;

namespace Caelan.FrameworksTest.Models.Mapping
{
	public class UserRoleMap : EntityTypeConfiguration<UserRole>
	{
		public UserRoleMap()
		{
			// Primary Key
			HasKey(t => t.ID);

			// Properties
			// Table & Column Mappings
			ToTable("UserRole");
			Property(t => t.ID).HasColumnName("Id");
			Property(t => t.IDUser).HasColumnName("IdUser");
			Property(t => t.IDRole).HasColumnName("IdRole");

			// Relationships
			HasRequired(t => t.Role)
				.WithMany(t => t.UserRoles)
				.HasForeignKey(d => d.IDRole);
			HasRequired(t => t.User)
				.WithMany(t => t.UserRoles)
				.HasForeignKey(d => d.IDUser);

		}
	}
}
