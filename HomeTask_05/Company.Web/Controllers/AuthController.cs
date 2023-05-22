using Company.Core.Interfaces;
using Company.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Company.Web.Controllers;

public class AuthController : BaseController
{
    public AuthController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    [Route("")]
    public IActionResult Index()
    {
        return RedirectToAction("List", "user");
    }

    [Route("[action]")]
    public IActionResult Login(string returnUrl = "")
    {
        var model = new LoginViewModel();
        model.ReturnUrl = returnUrl;
        return View(model);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        var user = _unitOfWork.UserRepo.FindByEmail(loginViewModel.Email);
        if (user == null || !user.VerifyPassword(loginViewModel.Password))
        {
            ModelState.AddModelError(string.Empty, loginViewModel.FailMessage);
            return View();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.Name),
            new("Department", user.Department.Name)
        };

        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookie"));

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { AllowRefresh = true });

        if (string.IsNullOrEmpty(loginViewModel.ReturnUrl) || loginViewModel.ReturnUrl.ToLower() == "/login")
        {
            return RedirectToAction("List", "User");
        }

        return Redirect(loginViewModel.ReturnUrl);
    }

    [Authorize]
    [Route("[action]")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
