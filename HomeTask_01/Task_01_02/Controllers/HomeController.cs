using Microsoft.AspNetCore.Mvc;

namespace Task_01.Controllers;

public class HomeController : Controller
{
    [Route("")]
    public IActionResult Index() => View();
}
