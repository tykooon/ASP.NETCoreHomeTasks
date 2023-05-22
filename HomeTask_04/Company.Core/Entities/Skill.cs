using System.Text.Json.Serialization;

namespace Company.Core.Entities;

public class Skill : Entity<int>
{
    private HashSet<Candidate> _candidates = new();

    public const int MaxNameLength = 32;

    public string Name { get; set; }

    [JsonIgnore]
    public IReadOnlyCollection<Candidate> Candidates => _candidates;
}