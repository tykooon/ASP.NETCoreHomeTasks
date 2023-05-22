using Microsoft.AspNetCore.Mvc;
using Task_02_1.Models;

namespace Task_02_1.Controllers;

public class CalendarController : Controller
{
    [Route("")]
    public IActionResult Index() => RedirectToAction("ShowMonth", new{
            year = DateTime.Now.Year,
            month = DateTime.Now.Month });

    [Route("{year:int:range(1900,2100)}/{month:int:range(1,12)}")]
    public IActionResult ShowMonth(int year, int month) =>
        View(new MonthViewModel(year, month));


    [Route("{year}/{**month}")]
    public IActionResult ShowError(string year, string month = null)
    {
        if (!int.TryParse(year, out int yearInt) || !IsCorrectYear(yearInt))
        {
            ViewBag.Error = "Bad <span class='error-accent'>year</span> value. Must be an integer number between 1900 and 2100.";
            return View();
        }
        if (month == null)
        {
            ViewBag.Error = "Missing <span class='error-accent'>month</span> value. Month parameter is requiered!";
            return View();
        }
        if (!int.TryParse(month, out int monthInt) || !IsCorrectMonth(monthInt))
        {
            ViewBag.Error = "Bad <span class='error-accent'>month</span> value. Must be an integer number between 1 and 12.";
            return View();
        }
        ViewBag.Error = "Sorry... Unexpected error. Try another URL.";
        return View();
    }

    private static bool IsCorrectYear(int year) => year >= 1900 && year <= 2100;

    private static bool IsCorrectMonth(int month) => month >= 1 && month <= 12;
}
