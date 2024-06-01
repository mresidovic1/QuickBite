using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickBite.Data;
using QuickBite.Models;

namespace QuickBite.Controllers
{
    public class PlaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlaceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Place
        public async Task<IActionResult> Index()
        {
            if (!_context.UsluznaJedinica.Any())
            {
                NapuniBazuPodataka();
            }
            var applicationDbContext = _context.UsluznaJedinica.Include(u => u.Proizvod);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Place/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluznaJedinica = await _context.UsluznaJedinica
                .Include(u => u.Proizvod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usluznaJedinica == null)
            {
                return NotFound();
            }

            return View(usluznaJedinica);
        }

        private void NapuniBazuPodataka()
        {
            var places = new List<UsluznaJedinica>
            {
                // Restorani
                new UsluznaJedinica { TipUsluge = Kategorija.Restorani, Naziv = "Arigato-sushi bar", Adresa = "Čobanija 1, Sarajevo 71000", ProizvodId = null },
                new UsluznaJedinica { TipUsluge = Kategorija.Restorani, Naziv = "Metropolis", Adresa = "Maršala Tita 21, Sarajevo 71000", ProizvodId = null },
                new UsluznaJedinica { TipUsluge = Kategorija.Restorani, Naziv = "Ćevabdžinica Hodžić", Adresa = "Bravadžiluk 34, Sarajevo 71000", ProizvodId = null },

                // Supermarketi
                new UsluznaJedinica { TipUsluge = Kategorija.Supermarketi, Naziv = "Amko Komerc", Adresa = "Butmirska cesta 20, Ilidža", ProizvodId = null },
                new UsluznaJedinica { TipUsluge = Kategorija.Supermarketi, Naziv = "Merkator", Adresa = "Ložionička 16, Sarajevo 71000", ProizvodId = null },
                new UsluznaJedinica { TipUsluge = Kategorija.Supermarketi, Naziv = "Bingo", Adresa = "Dzemala Bijedica St 160, Sarajevo 71000", ProizvodId = null },

                // Brza Hrana
                new UsluznaJedinica { TipUsluge = Kategorija.BrzaHrana, Naziv = "KFC", Adresa = "Vrbanja 1, Sarajevo 71000", ProizvodId = null },
                new UsluznaJedinica { TipUsluge = Kategorija.BrzaHrana, Naziv = "Bash", Adresa = "Kulovića br.5, Sarajevo 71000", ProizvodId = null },
                new UsluznaJedinica { TipUsluge = Kategorija.BrzaHrana, Naziv = "Burger King", Adresa = "Vrbanja 1, Sarajevo 71000", ProizvodId = null }
            };

            _context.UsluznaJedinica.AddRange(places);
            _context.SaveChanges();
        }

        // GET: Place/Create
        public IActionResult Create()
        {
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Id");
            return View();
        }

        // POST: Place/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipUsluge,Naziv,Adresa,ProizvodId")] UsluznaJedinica usluznaJedinica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usluznaJedinica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Id", usluznaJedinica.ProizvodId);
            return View(usluznaJedinica);
        }

        // GET: Place/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluznaJedinica = await _context.UsluznaJedinica.FindAsync(id);
            if (usluznaJedinica == null)
            {
                return NotFound();
            }
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Id", usluznaJedinica.ProizvodId);
            return View(usluznaJedinica);
        }

        // POST: Place/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipUsluge,Naziv,Adresa,ProizvodId")] UsluznaJedinica usluznaJedinica)
        {
            if (id != usluznaJedinica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usluznaJedinica);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsluznaJedinicaExists(usluznaJedinica.Id))
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
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Id", usluznaJedinica.ProizvodId);
            return View(usluznaJedinica);
        }

        // GET: Place/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluznaJedinica = await _context.UsluznaJedinica
                .Include(u => u.Proizvod)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usluznaJedinica == null)
            {
                return NotFound();
            }

            return View(usluznaJedinica);
        }

        // POST: Place/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usluznaJedinica = await _context.UsluznaJedinica.FindAsync(id);
            if (usluznaJedinica != null)
            {
                _context.UsluznaJedinica.Remove(usluznaJedinica);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FilterByCategory(Kategorija kategorija)
        {
            var filteredPlaces = await _context.UsluznaJedinica
                .Where(u => u.TipUsluge == kategorija)
                .ToListAsync();

            return View("Index", filteredPlaces);
        }

        private bool UsluznaJedinicaExists(int id)
        {
            return _context.UsluznaJedinica.Any(e => e.Id == id);
        }
    }
}
