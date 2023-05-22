using Company.Core.Interfaces;
using Company.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Web.Pages.Candidate;

public class EditModel : CandidatePageModel
{
    [BindProperty]
    public CandidateEditModel Candidate { get; set; }

    public EditModel(IUnitOfWork unitOfWork) : base(unitOfWork)
    { }

    public IActionResult OnGet(Guid id)
    {
        var candidate = _unitOfWork.CandidateRepo.Find(id);
        if (candidate is null)
        {
            return NotFound();
        }

        Candidate = new CandidateEditModel(candidate);
        BuildLists();
        return Page();
    }

    public IActionResult OnPost(Guid id)
    {
        if (!ModelState.IsValid)
        {
            BuildLists();
            return Page();
        }

        var candidate = _unitOfWork.CandidateRepo.Find(id);
        if (candidate is null)
        {
            return NotFound();
        }

        var position = Candidate.PositionId.HasValue
            ? _unitOfWork.PositionRepo.Find(Candidate.PositionId.Value)
            : null;
        candidate.Update(Candidate.FullName, Candidate.Email, Candidate.Phone, Candidate.StartDate, Candidate.LinkedIn, Candidate.Experience, position);

        var skillIds = new HashSet<int>(Candidate.SkillIds);
        var skills = _unitOfWork.SkillRepo.GetAll().Where(x => skillIds.Contains(x.Id)).ToArray();
        candidate.ClearSkills();
        candidate.AddSkills(skills);
        _unitOfWork.Commit();
        return RedirectToPage("Index");
    }

    private void BuildLists()
    {
        Candidate.Positions = new SelectList(_unitOfWork.PositionRepo.GetAll(), "Id", "Name");
        Candidate.Skills = new SelectList(_unitOfWork.SkillRepo.GetAll(), "Id", "Name");
    }
}
