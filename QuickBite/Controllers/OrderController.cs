using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickBite.Data;
using QuickBite.Models;

namespace QuickBite.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var narudzba = await _context.Narudzba
                .Include(n => n.Korisnik)
                .Include(n => n.UsluznaJedinica)
                .Include(n => n.Naplata)
                .Where(n => n.KorisnikId == currentUser.Id && n.VrijemeNarudzbe == 0)
                .OrderByDescending(n => n.Id)  // Assuming Id is the primary key
                .FirstOrDefaultAsync();

            if (narudzba == null)
            {
                return View("EmptyOrder");
            }

            var proizvodiNarudzbe = await _context.ProizvodNarudzba
                .Include(pn => pn.Proizvod)
                .Where(pn => pn.NarudzbaId == narudzba.Id)
                .ToListAsync();

            var viewModel = new NarudzbaViewModel
            {
                Narudzba = narudzba,
                ProizvodiNarudzbe = proizvodiNarudzbe
            };

            return View(viewModel);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .Include(n => n.Korisnik)
                .Include(n => n.Naplata)
                .Include(n => n.UsluznaJedinica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id");
            ViewData["NaplataId"] = new SelectList(_context.Naplata, "Id", "Id");
            ViewData["UsluznaJedinicaId"] = new SelectList(_context.UsluznaJedinica, "Id", "Id");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Cijena,VrijemeNarudzbe,UsluznaJedinicaId,NaplataId,KorisnikId")] Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                _context.Add(narudzba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KorisnikId"] = new SelectList(_context.Korisnik, "Id", "Id", narudzba.KorisnikId);
            ViewData["NaplataId"] = new SelectList(_context.Naplata, "Id", "Id", narudzba.NaplataId);
            ViewData["UsluznaJedinicaId"] = new SelectList(_context.UsluznaJedinica, "Id", "Id", narudzba.UsluznaJedinicaId);
            return View(narudzba);
        }

        // GET: Order/Edit/5
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
            ViewData["UsluznaJedinicaId"] = new SelectList(_context.UsluznaJedinica, "Id", "Id", narudzba.UsluznaJedinicaId);
            return View(narudzba);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cijena,VrijemeNarudzbe,UsluznaJedinicaId,NaplataId,KorisnikId")] Narudzba narudzba)
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
            ViewData["UsluznaJedinicaId"] = new SelectList(_context.UsluznaJedinica, "Id", "Id", narudzba.UsluznaJedinicaId);
            return View(narudzba);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .Include(n => n.Korisnik)
                .Include(n => n.Naplata)
                .Include(n => n.UsluznaJedinica)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirm()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Pronalazi trenutnu narudžbu korisnika
            var currentOrder = await _context.Narudzba
                .Where(n => n.KorisnikId == currentUser.Id && n.VrijemeNarudzbe == 0)
                .OrderByDescending(n => n.Id)
                .FirstOrDefaultAsync();

            if (currentOrder == null)
            {
                // Ako trenutna narudžba ne postoji, vratimo se na početnu stranicu
                return RedirectToAction("Index", "Home");
            }

            var naplata = new Naplata
            {
                VrstaNaplate = VrstaNaplate.Kartica, // Zamijenite s pravim tipom plaćanja
                BrojKartice = 1111111,
                Napomena = "Najnovija naplata"
            };
            _context.Naplata.Add(naplata);
            await _context.SaveChangesAsync();

            // Ažuriramo vrijednost VrijemeNarudzbe na trenutni datum i vrijeme
            currentOrder.VrijemeNarudzbe = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // Ažuriramo trenutnu narudžbu u bazi podataka
            _context.Narudzba.Update(currentOrder);
            await _context.SaveChangesAsync();

            return RedirectToAction("MapView", "Home");
        }


        // POST: Order/Delete/5
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
