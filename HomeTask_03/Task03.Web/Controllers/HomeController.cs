using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers;

public class HomeController : Controller
{
    [Route("")]
    public IActionResult Index()
    {
        return RedirectToAction("List","candidate");
    }
}
