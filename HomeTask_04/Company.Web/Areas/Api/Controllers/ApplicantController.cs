using Company.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Company.Web.Areas.Api.Controllers;

[Route("api/applicants")]
[ApiController]
public class ApplicantController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public ApplicantController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet("")]
    public IActionResult GetFiltered([FromQuery] string skills = null, string sort = null)
    {
        var list = _unitOfWork.CandidateRepo.GetAll();
        if (skills != null)
        {
            var skillList = skills.ToLower().Split(',');
            list = list.Where(x => skillList.Except(x.Skills.Select(s => s.Name.ToLower())).Count() == 0);
        }
        if (sort != null)
        {
            switch (sort)
            {
                case "name":
                case "+name":
                    list = list.OrderBy(x => x.FullName);
                    break;
                case "-name":
                    list = list.OrderByDescending(x => x.FullName);
                    break;
                case "date":
                case "+date":
                    list = list.OrderBy(x => x.StartDate);
                    break;
                case "-date":
                    list = list.OrderByDescending(x => x.StartDate);
                    break;
                default:
                    break;
            }
        }
        return new JsonResult(list);
    }
}
