using BlogProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogProject.DataAccess.Concrete.EntityFrameworkCore.Mapping
{
    public class AppUserMap : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Username).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Surname).HasMaxLength(100);

            builder.HasMany(x => x.Blogs).WithOne(x => x.AppUser).HasForeignKey(x => x.AppUserId);
        }
    }
}
