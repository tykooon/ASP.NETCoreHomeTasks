using Company.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.Config;

internal sealed class PositionConfig : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable(nameof(Position));

        //builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Position.MaxNameLength);
    }
}
