using Company.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.Web.Areas.Admin.Models;

public class PasswordEditViewModel
{
    public Guid Id { get; set; }

    [BindNever]
    public string FullName { get; set; }

    [Required]
    [PasswordPropertyText]
    [StringLength(User.MaxPasswordLength)]
    [DisplayName("New Password")]
    public string NewPassword { get; set; }
}
