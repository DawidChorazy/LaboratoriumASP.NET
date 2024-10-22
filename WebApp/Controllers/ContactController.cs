using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;

public class ContactController : Controller
{
    private static Dictionary<int, ContactModel> _contacts = new()
    {
        {1, new ContactModel() {Id = 1, FirstName = "Adam", LastName = "Abecki", Email = "adam@wsei.edu.pl", Birth = new DateOnly(2000,10,10), PhoneNumber = "+48 222 222 333"}},
        {2, new ContactModel() {Id = 2, FirstName = "Damian", LastName = "Wysoki", Email = "damian@wsei.edu.pl", Birth = new DateOnly(2004,11,18), PhoneNumber = "+48 423 525 167" }},
        {3, new ContactModel() {Id = 3, FirstName = "Katarzyna", LastName = "Tuba", Email = "katarzyna@wsei.edu.pl", Birth = new DateOnly(1999,03,04), PhoneNumber = "+48 927 345 867" }}
    };

    private static int currentId = 3;
    
    // Contact list
    public IActionResult Index()
    {
        return View(_contacts);
    }

    // Adding contact form
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }
    // Getting and saving new contact
    [HttpPost]
    public IActionResult Add(ContactModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        else
        {
            model.Id = ++currentId;
            _contacts.Add(model.Id , model);
            return View("Index", _contacts);
        }
    }

    public IActionResult Delete(int id)
    {
        _contacts.Remove(id);
        return View("Index", _contacts);
    }
    
}