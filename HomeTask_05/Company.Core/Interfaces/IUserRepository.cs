using Company.Core.Entities;

namespace Company.Core.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
    public User FindByEmail(string email);
}
