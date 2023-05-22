using Company.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.Config;

internal sealed class DepartmentConfig : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable(nameof(Department));

        builder.Property(x => x.Name).IsRequired().HasMaxLength(Department.MaxNameLength);
    }
}
