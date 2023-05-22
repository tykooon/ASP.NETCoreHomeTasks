using Entities = Company.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Company.Core.Interfaces;

namespace Company.Web.Pages.Candidate
{
    public class DetailsModel : CandidatePageModel
    {
        public Entities.Candidate Candidate { get; set; }

        public DetailsModel(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public IActionResult OnGet(Guid id)
        {
            Candidate = _unitOfWork.CandidateRepo.Find(id);
            return Candidate == null
                ? NotFound()
                : Page();
        }
    }
}
