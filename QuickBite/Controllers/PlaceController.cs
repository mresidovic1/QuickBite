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

        private bool UsluznaJedinicaExists(int id)
        {
            return _context.UsluznaJedinica.Any(e => e.Id == id);
        }
    }
}
