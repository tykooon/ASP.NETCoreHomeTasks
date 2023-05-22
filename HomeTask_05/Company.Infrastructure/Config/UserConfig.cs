using Company.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Company.Infrastructure.Config;

internal sealed class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));

        //builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            guid => guid.ToString().ToUpper(),
            str => Guid.Parse(str)
            );
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(User.MaxEmailLength);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(User.MaxPasswordLength);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(User.MaxFullNameLength);
        builder.Property(x => x.BirthDate).IsRequired().HasConversion(
            date => date.ToString("yyyy-MM-dd"),
            date => DateOnly.Parse(date));

        builder.HasOne(x => x.Department).WithMany(x => x.Users).HasForeignKey(x => x.DepartmentId).IsRequired();

        builder.HasOne(x => x.Role).WithMany().OnDelete(DeleteBehavior.SetNull);
    }
}
