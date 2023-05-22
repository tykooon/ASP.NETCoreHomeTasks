using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public abstract class Repository<TEntity, TKey> : Repository<TEntity>, IRepository<TEntity, TKey> where TEntity : Entity<TKey>
{
    protected Repository(DbContext context) : base(context) { }

    public virtual TEntity Find(TKey id) => Find(x => x.Id.Equals(id));
}
