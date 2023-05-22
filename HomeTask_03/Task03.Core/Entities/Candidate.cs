namespace Company.Core.Entities;

public class Candidate : Entity<Guid>
{
    public const int MaxFullNameLength = 128;
    public const int MaxEmailLength = 128;
    public const int MaxPhoneLength = 16;
    public const int MaxLinkedinUriLength = 1024;
    public const int MaxExperience = 20;

    private HashSet<Skill> _skills = new();

    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateOnly StartDate { get; set; }
    public string LinkedIn { get; set; }
    public int? Experience { get; set; }

    public int? PositionId { get; set; }
    public Position TargetPosition { get; set; }

    public IReadOnlyCollection<Skill> Skills => _skills;

    public Candidate(string fullName, string email, string phone, DateOnly startDate)
    {
        FullName = fullName;
        Email = email;
        Phone = phone;
        StartDate = startDate;
    }

    private Candidate() { }

    public void Update(string fullName, string email, string phone, string linkedIn, int? experience, Position position)
    {
        FullName = fullName;
        Email = email;
        Phone = phone;
        LinkedIn = linkedIn;
        Experience = experience;
        TargetPosition = position;
    }

    public void SetPosition(Position position)
    {
        TargetPosition = position;
    }

    public void AddSkills(params Skill[] skills)
    {
        foreach (var skill in skills)
        {
            _skills.Add(skill);
        }
    }

    public void RemoveSkill(Skill skill)
    {
        _skills.Remove(skill);
    }

    public void ClearSkills()
    {
        _skills.Clear();
    }
}