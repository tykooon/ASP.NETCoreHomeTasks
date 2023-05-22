using Company.Core.Interfaces;
using Company.Web.Helpers;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Web.Controllers;

[Route("[controller]")]
public class UserController : BaseController
{
    public UserController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

[Route("")]
    public IActionResult List()
    {
        var list = _unitOfWork.UserRepo.GetAll();
        return View(list);
    }

    [HttpGet("{id:guid}")]
    [Authorize("LoggedInUser")]
    public IActionResult Details(Guid id)
    {
        var cand = _unitOfWork.UserRepo.Find(id);
        return cand == null ? NotFound() : View(cand);
    }

    [HttpGet("edit/{id:guid}")]
    [Authorize("AdminDepartmentUser")]
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
    [Authorize("AdminDepartmentUser")]
    public IActionResult Edit(UserEditModel model)
    {
        if(!ValidationHelper.IsValidModel(ModelState, EditAccess.Basic))
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

        _unitOfWork.Commit();
        return RedirectToAction("List");
    }

    private void BuildLists(UserEditModel model)
    {
        model.Departments = new SelectList(_unitOfWork.DepartmentRepo.GetAll(), "Id", "Name");
        model.Roles = new SelectList(_unitOfWork.RoleRepo.GetAll(), "Id", "Name");
    }
}
