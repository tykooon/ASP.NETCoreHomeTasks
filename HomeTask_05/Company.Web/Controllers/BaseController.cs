using Company.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers;

public abstract class BaseController : Controller
{
    protected IUnitOfWork _unitOfWork;

    public BaseController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
}
