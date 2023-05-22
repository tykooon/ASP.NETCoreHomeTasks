using Company.Core.Entities;
using Company.Core.Helpers;
using Company.Core.Interfaces;
using Company.Web.Areas.Admin.Models;
using Company.Web.Helpers;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Company.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("[area]/[controller]")]
[Authorize("AdminRole")]
public class UserController : Controller
{
    private IUnitOfWork _unitOfWork;

    public UserController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet("add")]
    public IActionResult Add()
    {
        var model = new UserEditModel();
        BuildLists(model);
        return View(model);
    }

    [HttpPost("add")]
    public IActionResult Add(UserEditModel model)
    {
        if (!ValidationHelper.IsValidModel(ModelState, EditAccess.Full))
        {
            BuildLists(model);
            return View(model);
        }

        var user = new User(model.FullName, model.Email, HashHelper.GetHash(model.NewPassword), model.BirthDate, model.DepartmentId);
        user.SetRole(model.RoleId);

        _unitOfWork.UserRepo.Add(user);
        _unitOfWork.Commit();
        return RedirectToAction("List", "User", new { Area = "" });
    }

    [HttpGet("edit/{id:guid}")]
    public IActionResult Edit(Guid id)
    {
        var user = _unitOfWork.UserRepo.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        var model = new UserEditModel(user);
        BuildLists(model);
        return View(model);
    }

    [HttpPost("edit/{id:guid}")]
    public IActionResult Edit(UserEditModel model)
    {
        if (!ValidationHelper.IsValidModel(ModelState,EditAccess.Admin))
        {
            BuildLists(model);
            return View(model);
        }

        var user = _unitOfWork.UserRepo.Find(model.Id);
        if (user is null)
        {
            return NotFound();
        }

        user.Update(model.FullName, model.DepartmentId, model.BirthDate);
        user.SetRole(model.RoleId);
        user.ChangeEmail(model.Email);

        _unitOfWork.Commit();
        return RedirectToAction("List", "User", new { Area = "" });
    }

    [HttpPost("password")]
    public IActionResult ChangePassword([FromBody] PasswordEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = _unitOfWork.UserRepo.Find(model.Id);
        if (user is null)
        {
            return BadRequest();
        }

        user.ChangePassword(model.NewPassword);
        _unitOfWork.Commit();
        return Ok();
    }


        [HttpDelete("")]
    [Authorize("AdminRole")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        var user = _unitOfWork.UserRepo.Find(id);
        if (user == null)
        {
            return BadRequest();
        }

        _unitOfWork.UserRepo.Delete(user);
        _unitOfWork.Commit();
        return Ok();
    }

    private void BuildLists(UserEditModel model)
    {
        model.Departments = new SelectList(_unitOfWork.DepartmentRepo.GetAll(), "Id", "Name");
        model.Roles = new SelectList(_unitOfWork.RoleRepo.GetAll(), "Id", "Name");
    }
}
