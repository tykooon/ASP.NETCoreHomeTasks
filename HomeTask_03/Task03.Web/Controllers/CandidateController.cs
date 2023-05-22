using Company.Core.Entities;
using Company.Core.Interfaces;
using Company.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Web.Controllers;

[Route("[controller]")]
public class CandidateController : Controller
{
    private IUnitOfWork _uow;

    public CandidateController(IUnitOfWork uow) => _uow = uow;

    [Route("")]
    public IActionResult List()
    {
        var list = _uow.CandidateRepo.GetAll();
        return View(list);
    }

    [HttpGet("{id:guid}")]
    public IActionResult Details(Guid id)
    {
        var cand = _uow.CandidateRepo.Find(id);
        return cand == null ? NotFound() : View(cand);
    }

    [HttpGet("add")]
    public IActionResult Add()
    {
        var model = new CandidateEditModel();
        BuildLists(model);
        return View(model);
    }

    [HttpPost("add")]
    public IActionResult Add(CandidateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            BuildLists(model);
            return View(model);
        }

        var candidate = new Candidate(model.FullName, model.Email, model.Phone, DateOnly.FromDateTime(DateTime.Today))
        {
            Experience = model.Experience,
            LinkedIn = model.LinkedIn,
            TargetPosition = model.PositionId.HasValue ? _uow.PositionRepo.Find(model.PositionId.Value) : null
    };

        var skillIds = new HashSet<int>(model.SkillIds);
        var skills = _uow.SkillRepo.GetAll().Where(x => skillIds.Contains(x.Id)).ToArray();
        candidate.AddSkills(skills);
        _uow.CandidateRepo.Add(candidate);
        _uow.Commit();
        return RedirectToAction(nameof(List));
    }

    [HttpGet("edit/{id:guid}")]
    public IActionResult Edit(Guid id)
    {
        var cand = _uow.CandidateRepo.Find(id);
        if (cand == null)
        {
            return NotFound();
        }

        var model = new CandidateEditModel(cand);
        BuildLists(model);
        return View(model);
    }

    [HttpPost("edit/{id:guid}")]
    public IActionResult Edit(CandidateEditModel model)
    {
        if (!ModelState.IsValid)
        {
            BuildLists(model);
            return View(model);
        }

        var candidate = _uow.CandidateRepo.Find(model.Id);
        if (candidate is null)
        {
            return NotFound();
        }

        var position = model.PositionId.HasValue ? _uow.PositionRepo.Find(model.PositionId.Value) : null;
        candidate.Update(model.FullName, model.Email, model.Phone, model.LinkedIn, model.Experience, position);
        
        var skillIds = new HashSet<int>(model.SkillIds);
        var skills = _uow.SkillRepo.GetAll().Where(x => skillIds.Contains(x.Id)).ToArray();
        candidate.ClearSkills();
        candidate.AddSkills(skills);
        _uow.Commit();
        return RedirectToAction(nameof(List));
    }

    [HttpDelete("")]
    public IActionResult Delete([FromQuery] Guid id)
    {
        var cand = _uow.CandidateRepo.Find(id);
        if (cand == null)
        {
            return BadRequest();
        }

        _uow.CandidateRepo.Delete(cand);
        _uow.Commit();
        return Ok();
    }

    private void BuildLists(CandidateEditModel model)
    {
        model.Positions = new SelectList(_uow.PositionRepo.GetAll(), "Id", "Name");
        model.Skills = new SelectList(_uow.SkillRepo.GetAll(), "Id", "Name");
    }
}
