namespace Task_02_2.Infrastructure;
using Task_02_2.Models;

public interface IContactRepo
{
    public IEnumerable<Contact> GetContacts();

    public Contact GetContactById(int id);
}
