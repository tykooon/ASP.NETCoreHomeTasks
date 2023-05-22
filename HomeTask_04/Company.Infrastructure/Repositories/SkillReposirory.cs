using Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Company.Infrastructure.Repositories;

public class SkillReposirory : Repository<Skill, int>, ISkillRepository
{
    internal SkillReposirory(DbContext context) : base(context)
    { }
}
