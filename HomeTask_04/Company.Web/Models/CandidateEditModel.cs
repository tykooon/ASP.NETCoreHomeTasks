using Company.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.Web.Models;

public class CandidateEditModel
{
    public Guid Id { get; set; }

    [Required]
    [StringLength(Candidate.MaxFullNameLength)]
    [DisplayName("Full Name")]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(Candidate.MaxEmailLength)]
    [DisplayName("E-mail")]
    public string Email { get; set; }

    [Required]
    [StringLength(Candidate.MaxPhoneLength)]
    [DisplayName("Phone")]
    public string Phone { get; set; }

    [Required]
    [DisplayName("Apply Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy-MM-dd}")]
    public DateOnly StartDate { get; set; }

    [StringLength(Candidate.MaxLinkedinUriLength)]
    [DisplayName("LinkedIn Link")]
    public string LinkedIn { get; set; }

    [Range(0,20)]
    public int? Experience { get; set; }

    [DisplayName("Target Position")]
    public int? PositionId { get; set; }

    public SelectList Positions { get; set; }

    [BindProperty]
    public int[] SkillIds { get; set; } = Array.Empty<int>();

    public SelectList Skills { get; set; }

    public CandidateEditModel()
    {
        StartDate = DateOnly.FromDateTime(DateTime.Now);
    }

    public CandidateEditModel(Candidate candidate)
    {
        ArgumentNullException.ThrowIfNull(candidate);

        Id = candidate.Id;
        FullName= candidate.FullName;
        Email= candidate.Email;
        Phone= candidate.Phone;
        StartDate = candidate.StartDate;
        LinkedIn= candidate.LinkedIn;
        Experience= candidate.Experience;
        PositionId= candidate.PositionId;

        SkillIds = candidate.Skills.Select(s => s.Id).ToArray(); 
    }

    public bool HasSkill(string skillId) => int.TryParse(skillId, out var id) && Array.IndexOf(SkillIds, id) != -1;
}
