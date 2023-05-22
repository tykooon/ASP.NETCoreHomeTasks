namespace Company.Core.Entities;

public class CandidateSkill : Entity
{
    public Guid CandidateId { get; set; }
    public int SkillId { get; set; }
}
