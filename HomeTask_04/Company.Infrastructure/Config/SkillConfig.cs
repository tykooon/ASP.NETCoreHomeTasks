using Company.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;

namespace Company.Infrastructure.Config;

internal sealed class SkillConfig : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable(nameof(Skill));

        //builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(Skill.MaxNameLength);

        //builder.HasMany(x => x.Candidates).WithMany(x => x.Skills)
        //    .UsingEntity<CandidateSkill>(
        //        right => right.HasOne<Candidate>().WithMany().HasForeignKey(x => x.CandidateId),
        //        left => left.HasOne<Skill>().WithMany().HasForeignKey(x => x.SkillId),
        //        join => join.ToTable("CandidateSkill"));
    }
}
