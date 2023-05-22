using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public sealed class CandidateRepository : Repository<Candidate, Guid>, ICandidateRepository
{
    internal CandidateRepository(DbContext context) : base(context) { }

    protected override IQueryable<Candidate> MakeInclusions()
    {
        return DbSet.Include(x => x.TargetPosition).Include(x => x.Skills);
    }
}
