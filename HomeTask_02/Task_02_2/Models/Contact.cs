namespace Task_02_2.Models;

public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public List<string> Phones { get; set; }
}
