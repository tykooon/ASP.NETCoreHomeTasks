using Company.Core.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Company.Web.Pages.Candidate;

public abstract class CandidatePageModel: PageModel
{
    protected IUnitOfWork _unitOfWork;

    public CandidatePageModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
}
