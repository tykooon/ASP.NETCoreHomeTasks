namespace Company.Core.Entities;

public class Department : Entity<int>
{
    private HashSet<User> _users = new();

    public const int MaxNameLength = 128;

    public string Name { get; set; }

    public IReadOnlyCollection<User> Users => _users;
}