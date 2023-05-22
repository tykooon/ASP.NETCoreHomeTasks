using Microsoft.AspNetCore.Mvc;

namespace Task_01.Controllers.Square;

[Route("[controller]")]
public class SquareController : Controller
{
    [Route("{**catch}")]
    public IActionResult Index() => View();

    //^-?[[0-9]]+\\.?[[0-9]]*$    for floats
    [Route("{number:regex(^-?[[0-9]]+$)}")]
    public IActionResult Square(string number)
    {
        ViewBag.Number = number;
        long temp = Convert.ToInt32(number);
        ViewBag.Result = temp * temp;
        return View();
    }
}
