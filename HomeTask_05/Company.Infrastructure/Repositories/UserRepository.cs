using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public sealed class UserRepository : Repository<User, Guid>, IUserRepository
{
    internal UserRepository(DbContext context) : base(context) { }

    public User FindByEmail(string email)
    {
        ArgumentNullException.ThrowIfNull(email, nameof(email));

        email = email.ToLower();
        return Find(x => x.Email.ToLower() == email);
    }

    protected override IQueryable<User> MakeInclusions()
    {
        return DbSet.Include(x => x.Department).Include(x => x.Role);
    }
}
