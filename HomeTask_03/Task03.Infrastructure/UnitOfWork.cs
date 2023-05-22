using Company.Core.Interfaces;
using Company.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Company.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private bool _isDisposed;
    private AppContext _context;
    private ICandidateRepository _candidateRepository;
    private IPositionRepository _positionRepository;
    private ISkillRepository _skillRepository;
    private AppContext Context => _context ??= new AppContext();

    public ICandidateRepository CandidateRepo => _candidateRepository ??= new CandidateRepository(Context);
    public IPositionRepository PositionRepo => _positionRepository ??= new PositionRepository(Context);
    public ISkillRepository SkillRepo => _skillRepository ??=new SkillReposirory(Context);

    public void Commit()
    {
        if (_context is null)
        {
            return;
        }

        if (_isDisposed)
        {
            throw new ObjectDisposedException("UnitOfWork");
        }

        try
        {
            Context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            //throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString()));
        }
        catch (DbUpdateException ex)
        {
            //throw new UpdateException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            //throw new RepositoryException("Commit error.", ex);
        }
    }

    public void Dispose()
    {
        if (_context is null || _isDisposed)
        {
            return;
        }

        _context.Dispose();
        _isDisposed = true;
    }
}
