namespace Company.Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepo { get; }
    IDepartmentRepository DepartmentRepo { get; }
    IRoleRepository RoleRepo { get; }

    void Commit();
}
