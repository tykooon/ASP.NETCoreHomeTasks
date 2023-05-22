namespace Company.Core.Entities;

public abstract class Entity<TKey> : Entity
{
    public TKey Id { get; set; }
}
