using Microsoft.AspNetCore.Mvc;
using Task_02_2.Infrastructure;

namespace Task_02_1.Controllers;

public class ContactController : Controller
{
    private IContactRepo _contactRepo;

    public ContactController(IContactRepo contactRepo)
    {
        _contactRepo = contactRepo;
    }

    [Route("")]
    public IActionResult Index() => RedirectToAction("List");

    [Route("contacts")]
    public IActionResult List() => View(_contactRepo.GetContacts());

    [Route("contacts/{id:int}")]
    public IActionResult Details(int id)
    {
        var cont = _contactRepo.GetContactById(id);
        return cont == null ? RedirectToRoute("404") : View(cont);
    }

    [Route("404",Name = "404")]
    public IActionResult PageNotFound() => View("Error404");
}
