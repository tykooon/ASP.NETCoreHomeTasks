namespace Company.Core.Entities;

public class Position : Entity<int>
{
    private HashSet<Candidate> _candidates = new();

    public const int MaxNameLength = 32;

    public string Name { get; set; }

    public IReadOnlyCollection<Candidate> Candidates => _candidates;
}