using Company.Core.Entities;

namespace Company.Core.Interfaces;

public interface IRepository<TEntity> where TEntity : Entity
{
    public IEnumerable<TEntity> GetAll();

    public void Add(TEntity entity);
    public void Update(TEntity entity);
    public void Delete(TEntity entity);
}
