using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public class PositionRepository : Repository<Position, int>, IPositionRepository
{
    internal PositionRepository(DbContext context) : base(context)
    { }
}
