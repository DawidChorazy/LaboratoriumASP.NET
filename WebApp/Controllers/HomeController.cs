using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Calculator(Operator? op, double? a, double? b)
    {
        //var op = Request.Query["op"];
        //var a = double.Parse(Request.Query["a"]);
        //var b = double.Parse(Request.Query["b"]);
        if (a is null || b is null)
        {
            ViewBag.ErrorMessage = "Incorrect format of a number";
            return View("CalculatorError");
        }

        if (op is null)
        {
            ViewBag.ErrorMessage = "Incorrect operator";
            return View("CalculatorError");
        }
        ViewBag.A = a;
        ViewBag.B = b;
        ViewBag.Operator = op;
        switch (op)
        {
            case Operator.Add:
                ViewBag.Result = a + b;
                ViewBag.Operator = "+";
                break;
            case Operator.Sub:
                ViewBag.Result = a - b;
                ViewBag.Operator = "-";
                break;
            case Operator.Mul:
                ViewBag.Result = a * b;
                ViewBag.Operator = "*";
                break;
            case Operator.Div:
                ViewBag.Result = a / b;
                ViewBag.Operator = "/";
                break;
        }
        return View();
    }

    public IActionResult Age(DateTime birth, DateTime future)
    {
        if (future < birth)
        {
            return View("CalculatorError");
        }
        ViewBag.Birth = birth;
        ViewBag.Future = future;
        
        int fAge = future.Year * 12;
        int bAge = birth.Year * 12;
        int age = (fAge - bAge)/12;
        
        ViewBag.Age = age;
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

public enum Operator
{
    Add, Sub, Mul, Div
}