using System.Data.Entity.ModelConfiguration;

namespace Caelan.FrameworksTest.Models.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("Role");
            Property(t => t.ID).HasColumnName("Id");
			Property(t => t.Description).HasColumnName("Description");
        }
    }
}
