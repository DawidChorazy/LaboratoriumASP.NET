using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models.Olympics;

namespace WebApp.Controllers
{
    public class OlympicsController : Controller
    {
        private readonly OlympicsContext _context;

        public OlympicsController(OlympicsContext context)
        {
            _context = context;
        }

        // GET: Olympics
        public IActionResult Index(int page = 1, int size = 10)
        {
            var totalCount = _context.People.Count();
            
            var peopleWithMedals = _context.People
                .OrderBy(p => p.FullName)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(person => new PersonViewModel
                {
                    Id = person.Id,
                    FullName = person.FullName,
                    Weight = person.Weight,
                    Height = person.Height,
                    Gender = person.Gender,
                    GoldMedals = _context.Medals.Count(m => m.Id == person.Id && m.MedalName == "Gold"),
                    SilverMedals = _context.Medals.Count(m => m.Id == person.Id && m.MedalName == "Silver"),
                    BronzeMedals = _context.Medals.Count(m => m.Id == person.Id && m.MedalName == "Bronze"),
                    EventCount = _context.CompetitorEvents.Count(e => e.CompetitorId == person.Id)
                });

            
            var pagingList = PagingListAsync<PersonViewModel>.Create(
                (p, s) => peopleWithMedals.AsAsyncEnumerable(),
                totalCount,
                page,
                size
            );

            return View(pagingList);
        }
        public IActionResult Events(int id)
        {
            var events = _context.CompetitorEvents
                .Where(ce => ce.CompetitorId == id)
                .Select(ce => new EventViewModel
                {
                    SportName = ce.Event.Sport != null ? ce.Event.Sport.SportName : "Brak danych",
                    EventName = ce.Event != null ? ce.Event.EventName : "Brak danych",
                    Medal = ce.Medal != null ? ce.Medal.MedalName : "Brak medalu"
                })
                .ToList();

            return View(events);
        }

        [HttpGet]
        public IActionResult AddEvent()
        {
            // Utworzenie pustego modelu, który będzie zawierał dane do formularza
            var model = new AddEventViewModel
            {
                // Pobranie list konkurentów, wydarzeń i medali
                Competitors = _context.People.ToList(),
                Events = _context.Events.ToList(),
                Medals = _context.Medals.ToList()
            };

            return View(model); // Przekazanie modelu do widoku
        }

        [HttpPost]
        public IActionResult AddEvent(AddEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Utworzenie nowego rekordu CompetitorEvent
                var competitorEvent = new CompetitorEvent
                {
                    CompetitorId = model.CompetitorId,
                    EventId = model.EventId,
                    MedalId = model.MedalId // Może być null, jeśli brak medalu
                };

                // Dodanie rekordu do kontekstu
                _context.CompetitorEvents.Add(competitorEvent);
                _context.SaveChanges();

                // Przekierowanie na stronę główną lub listę
                return RedirectToAction("Index");
            }

            // Jeśli model jest nieprawidłowy, odśwież widok z nowymi danymi
            model.Competitors = _context.People.ToList();
            model.Events = _context.Events.ToList();
            model.Medals = _context.Medals.ToList();

            // Przekazanie modelu z błędami walidacji z powrotem do widoku
            return View(model);
        }



        
        // GET: Olympics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Olympics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Olympics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Gender,Height,Weight")] Person person)
        {
            if (ModelState.IsValid)
            {
                var entity = _context.People.Add(new Person()
                {
                    Id = _context.People.Max(p => p.Id) + 1,
                    FullName = person.FullName,
                    Gender = person.Gender,
                    Height = person.Height,
                    Weight = person.Weight
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(person);
        }

        // GET: Olympics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Olympics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Gender,Height,Weight")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Olympics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Olympics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person != null)
            {
                _context.People.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
