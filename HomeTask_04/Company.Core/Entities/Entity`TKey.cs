using System.Text.Json.Serialization;

namespace Company.Core.Entities;

public abstract class Entity<TKey> : Entity
{
    [JsonIgnore]
    public TKey Id { get; set; }
}
