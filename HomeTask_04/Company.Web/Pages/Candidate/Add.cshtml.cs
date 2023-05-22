using Company.Core.Interfaces;
using Entities = Company.Core.Entities;
using Company.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Company.Web.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Web.Pages.Candidate
{
    [ApplyDateModify]
    public class AddModel : CandidatePageModel
    {
        [BindProperty]
        public CandidateEditModel Candidate { get; set; }

        public AddModel(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public void OnGet()
        {
            Candidate = new CandidateEditModel();
            BuildLists();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                BuildLists();
                return Page();
            }

            var candidate = new Entities.Candidate(Candidate.FullName, Candidate.Email, Candidate.Phone, Candidate.StartDate)
            {
                Experience = Candidate.Experience,
                LinkedIn = Candidate.LinkedIn,
                TargetPosition = Candidate.PositionId.HasValue ? _unitOfWork.PositionRepo.Find(Candidate.PositionId.Value) : null
            };

            var skillIds = new HashSet<int>(Candidate.SkillIds);
            var skills = _unitOfWork.SkillRepo.GetAll().Where(x => skillIds.Contains(x.Id)).ToArray();
            candidate.AddSkills(skills);
            _unitOfWork.CandidateRepo.Add(candidate);
            _unitOfWork.Commit();
            return RedirectToPage("Index");
        }

        private void BuildLists()
        {
            Candidate.Positions = new SelectList(_unitOfWork.PositionRepo.GetAll(), "Id", "Name");
            Candidate.Skills = new SelectList(_unitOfWork.SkillRepo.GetAll(), "Id", "Name");
        }
    }
}
