using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected DbSet<TEntity> DbSet;

    protected Repository(DbContext context)
    {
        DbSet = context.Set<TEntity>();
    }


    public void Add(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));
        DbSet.Add(entity);
    }

    public void Delete(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));
        DbSet.Remove(entity);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return MakeInclusions().ToList();
    }

    public void Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(nameof(entity));
        DbSet.Update(entity);
    }

    protected virtual IQueryable<TEntity> MakeInclusions() => DbSet;
}
