using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public class RoleReposirory : Repository<Role, int>, IRoleRepository
{
    internal RoleReposirory(DbContext context) : base(context)
    { }
}
