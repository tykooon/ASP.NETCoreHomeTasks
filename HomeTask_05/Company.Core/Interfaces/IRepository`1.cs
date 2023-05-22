using Company.Core.Entities;
using System.Linq.Expressions;

namespace Company.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    public IEnumerable<TEntity> GetAll();

    TEntity Find(Expression<Func<TEntity, bool>> predicate);
    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);
}
