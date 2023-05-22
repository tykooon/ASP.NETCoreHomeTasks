using Microsoft.AspNetCore.Mvc;

namespace Task_01.Controllers;

[Route("[controller]")]
public class PersonController : Controller
{
    [Route("{**catch}")]
    public IActionResult Index() => View();

    [Route("{name:nameTemplate}/{age:min(1):max(120)}")]
    public IActionResult ShowInfo(string name, int age)
    {
        ViewBag.Name = name;
        ViewBag.Age = age;

        return View();
    }
}
