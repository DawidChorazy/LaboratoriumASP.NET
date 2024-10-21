using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class BirthController : Controller
{
    public IActionResult Form()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Result(Birth model)
    {
        if (!model.IsValid())
        {
            return View("Error");
        }

        int age = model.CalculateAge();
        ViewBag.Message = $"Cześć {model.Name}, masz {age} lat(a).";
        return View();
    }
}