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
    public class KurirController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KurirController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kurir
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Narudzba.Include(n => n.Korisnik).Include(n => n.Naplata);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Kurir/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .Include(n => n.Korisnik)
                .Include(n => n.Naplata)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: Kurir/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id");
            ViewData["NaplataId"] = new SelectList(_context.Naplata, "Id", "Id");
            return View();
        }

        // POST: Kurir/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cijena,VrijemeNarudzbe,NaplataId,KorisnikId")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narudzba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", narudzba.KorisnikId);
            ViewData["NaplataId"] = new SelectList(_context.Naplata, "Id", "Id", narudzba.NaplataId);
            return View(narudzba);
        }

        // GET: Kurir/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", narudzba.KorisnikId);
            ViewData["NaplataId"] = new SelectList(_context.Naplata, "Id", "Id", narudzba.NaplataId);
            return View(narudzba);
        }

        // POST: Kurir/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cijena,VrijemeNarudzbe,NaplataId,KorisnikId")] Narudzba narudzba)
        {
            if (id != narudzba.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.Id))
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
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", narudzba.KorisnikId);
            ViewData["NaplataId"] = new SelectList(_context.Naplata, "Id", "Id", narudzba.NaplataId);
            return View(narudzba);
        }

        // GET: Kurir/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .Include(n => n.Korisnik)
                .Include(n => n.Naplata)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: Kurir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba != null)
            {
                _context.Narudzba.Remove(narudzba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(int id)
        {
            return _context.Narudzba.Any(e => e.Id == id);
        }
    }
}
