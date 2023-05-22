using Company.Core.Exceptions;
using Company.Core.Interfaces;
using Company.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data;

namespace Company.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private bool _isDisposed;
    private UnitOfWorkOptions _options;
    private AppContext _context;
    private IUserRepository _userRepository;
    private IDepartmentRepository _departmentRepository;
    private IRoleRepository _roleRepository;
    private AppContext Context => _context ??= new AppContext(_options.DbSource, _options.EnableDbLogging);

    public IUserRepository UserRepo => _userRepository ??= new UserRepository(Context);
    public IDepartmentRepository DepartmentRepo => _departmentRepository ??= new DepartmentRepository(Context);
    public IRoleRepository RoleRepo => _roleRepository ??= new RoleReposirory(Context);

    public UnitOfWork(IOptions<UnitOfWorkOptions> options)
    {
        _options = options.Value;
    }

    public UnitOfWork(UnitOfWorkOptions options)
    {
        _options = options;
    }

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
            throw new ConcurrencyException(ex.Entries.Select(x => x.Entity.ToString()));
        }
        catch (DbUpdateException ex)
        {
            throw new UpdateException(ex.Message, ex);
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Commit error.", ex);
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
