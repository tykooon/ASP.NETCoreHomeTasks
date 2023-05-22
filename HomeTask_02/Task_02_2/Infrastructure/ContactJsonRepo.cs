using Task_02_2.Models;
using System.Text.Json;

namespace Task_02_2.Infrastructure;

public class ContactJsonRepo : IContactRepo
{
    const string sourceFile = "contacts.json";

    private readonly List<Contact> _contacts = new();

    public ContactJsonRepo()
    {
        var json = File.ReadAllText(sourceFile);
        var jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
        _contacts = JsonSerializer.Deserialize<List<Contact>>(json, jsonOptions) ?? new List<Contact>();
    }

    public Contact GetContactById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

    public IEnumerable<Contact> GetContacts() => _contacts;
}
