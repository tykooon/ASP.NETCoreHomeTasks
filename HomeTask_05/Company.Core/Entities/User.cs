using Company.Core.Helpers;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Core.Entities;

public class User : Entity<Guid>
{
    public const int MaxFullNameLength = 128;
    public const int MaxEmailLength = 128;
    public const int MaxPasswordLength = 128;

    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public DateOnly BirthDate { get; private set; }

    public int DepartmentId { get; set; }
    public Department Department { get; }

    public int? RoleId { get; private set; }
    [ForeignKey("RoleId")]
    public Role Role { get; }


    public User(string fullName, string email, string passwordHash, DateOnly birthDate, int departmentId)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        BirthDate = birthDate;
        DepartmentId = departmentId;
    }

    private User() { }

    public void Update(string fullName, int departmentId, DateOnly birthDate)
    {
        FullName = fullName;
        DepartmentId = departmentId;
        BirthDate= birthDate;
    }

    public void SetRole(int? roleId)
    {
        RoleId = roleId;
    }

    public void ChangePassword(string password) => PasswordHash = HashHelper.GetHash(password);

    public void ChangeEmail(string email) => Email = email;

    public bool VerifyPassword(string password) =>
        !string.IsNullOrEmpty(password) && HashHelper.GetHash(password) == PasswordHash;
}