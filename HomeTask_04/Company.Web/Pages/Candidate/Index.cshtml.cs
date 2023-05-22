using Entities = Company.Core.Entities;
using Company.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Pages.Candidate;

public class IndexModel : CandidatePageModel
{
    public IEnumerable<Entities.Candidate> List => _unitOfWork.CandidateRepo.GetAll();

    public IndexModel(IUnitOfWork unitOfWork) : base(unitOfWork) { }

    public void OnGet()
    { }

    public IActionResult OnPostDelete(Guid id)
    {
        var candidate = _unitOfWork.CandidateRepo.Find(id);
        if (candidate == null)
        {
            return NotFound();
        }
        _unitOfWork.CandidateRepo.Delete(candidate);
        _unitOfWork.Commit();
        return RedirectToPage("Index");
    }
}
