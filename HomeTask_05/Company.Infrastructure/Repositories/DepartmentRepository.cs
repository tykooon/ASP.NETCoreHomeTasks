using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public class DepartmentRepository : Repository<Department, int>, IDepartmentRepository
{
    internal DepartmentRepository(DbContext context) : base(context)
    { }
}
