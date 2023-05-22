using System.ComponentModel.DataAnnotations;
using Company.Core.Entities;

namespace Company.Web.Models;

public class LoginViewModel
{
    public string FailMessage => "Authorization failed. Please try again.";

    [Required]
    [EmailAddress]
    [StringLength(User.MaxEmailLength, ErrorMessage = "Enter valid e-mail address.")]
    [Display(Prompt = "Enter your e-mail")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(User.MaxPasswordLength, ErrorMessage = $"Password is too long. Try again")]
    [Display(Prompt = "Enter your password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; } = string.Empty;
}
