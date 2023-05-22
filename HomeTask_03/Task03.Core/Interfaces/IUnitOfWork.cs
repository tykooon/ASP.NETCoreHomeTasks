namespace Company.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICandidateRepository CandidateRepo { get; }
    IPositionRepository PositionRepo { get; }
    ISkillRepository SkillRepo { get; }

    void Commit();
}
