using Company.Core.Entities;

namespace Company.Core.Interfaces;

public interface IRepository<TEntity, in TKey> : IRepository<TEntity> where TEntity : Entity<TKey>
{
    public TEntity Find(TKey key);
}
