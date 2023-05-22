using Company.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Company.Infrastructure.Config;

internal sealed class CandidateConfig : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable(nameof(Candidate));

        //builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            guid => guid.ToString().ToUpper(),
            str => Guid.Parse(str)
            );
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(Candidate.MaxFullNameLength);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(Candidate.MaxEmailLength);
        builder.Property(x => x.Phone).IsRequired().HasMaxLength(Candidate.MaxPhoneLength);
        builder.Property(x => x.StartDate).IsRequired().HasConversion(
            date => date.ToString("yyyy-MM-dd"),
            date => DateOnly.Parse(date));
        builder.ToTable(t => {
            t.HasCheckConstraint("CK_Experience", "[Experience] <= 20");
            t.HasCheckConstraint("CK_Experience", "[Experience] >= 0");
        });


        builder.HasMany(x => x.Skills).WithMany(x => x.Candidates)
            .UsingEntity<CandidateSkill>(
                right => right.HasOne<Skill>().WithMany().HasForeignKey(x => x.SkillId),
                left => left.HasOne<Candidate>().WithMany().HasForeignKey(x => x.CandidateId),
                join => join.ToTable("CandidateSkill"));
    }
}
