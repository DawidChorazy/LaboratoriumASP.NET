﻿using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class CalculatorController : Controller
{

    public IActionResult Result(Calculator model)
    {
        if (!model.IsValid())
        {
            return View("Error");
        }
        return View(model);
    }

    public IActionResult Form()
    {
        return View();
    }
    
    public enum Operators
    {
        Add, Sub, Mul, Div
    }
    
}