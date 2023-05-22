using Company.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models;

public class UserEditModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(User.MaxFullNameLength)]
    [DisplayName("Full Name")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(User.MaxEmailLength)]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required]
    [PasswordPropertyText]
    [StringLength(User.MaxPasswordLength)]
    [DisplayName("New Password")]
    public string NewPassword { get; set; }

    [Required]
    [DisplayName("Date of Birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateOnly BirthDate { get; set; }

    [DisplayName("Department")]
    public int DepartmentId { get; set; }

    public SelectList Departments { get; set; }

    [DisplayName("Role")]
    public int? RoleId { get; set; }

    public SelectList Roles { get; set; }

    public UserEditModel()
    { }

    public UserEditModel(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        Id = user.Id;
        FullName = user.FullName;
        Email = user.Email;
        DepartmentId = user.DepartmentId;
        RoleId = user.RoleId;
        BirthDate = user.BirthDate;
    }
}
