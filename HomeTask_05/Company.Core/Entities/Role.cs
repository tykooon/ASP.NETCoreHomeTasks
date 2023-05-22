namespace Company.Core.Entities;

public class Role : Entity<int>
{
    public const int MaxNameLength = 32;

    public string Name { get; set; }
}