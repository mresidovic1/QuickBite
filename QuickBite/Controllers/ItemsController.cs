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
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public ItemsController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Items
        public async Task<IActionResult> Index(int? usluznaJedinicaId)
        {
            if (usluznaJedinicaId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var items = _context.Proizvod
                .Include(p => p.Dodatak)
                .Where(p => p.UsluznaJedinicaId == usluznaJedinicaId);

            return View(await items.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> DodajUNarudzbu(int proizvodId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var proizvod = await _context.Proizvod.FindAsync(proizvodId);
            if (proizvod == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .FirstOrDefaultAsync(n => n.KorisnikId == currentUser.Id && n.VrijemeNarudzbe == 0);

            if (narudzba == null)
            {
                var naplata = new Naplata
                {
                    VrstaNaplate = VrstaNaplate.Gotovina, // Replace with the actual payment type
                    BrojKartice = 000000000, // Set an appropriate value or leave as 0
                    Napomena = "Nova naplata" // Add an appropriate note or leave as empty
                };
                _context.Naplata.Add(naplata);
                await _context.SaveChangesAsync();

                narudzba = new Narudzba
                {
                    KorisnikId = currentUser.Id,
                    UsluznaJedinicaId = proizvod.UsluznaJedinicaId,
                    VrijemeNarudzbe = 0,
                    Cijena = proizvod.Cijena ?? 0, // Set the initial price to the price of the first added product
                    NaplataId = naplata.Id
                };
                _context.Narudzba.Add(narudzba);
                await _context.SaveChangesAsync();
            }
            else
            {
                narudzba.Cijena += proizvod.Cijena ?? 0; // Add the price of the new product to the total order price
            }

            var proizvodNarudzba = new ProizvodNarudzba
            {
                ProizvodId = proizvod.Id,
                NarudzbaId = narudzba.Id
            };
            _context.ProizvodNarudzba.Add(proizvodNarudzba);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { usluznaJedinicaId = narudzba.UsluznaJedinicaId });
        }


        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod
                .Include(p => p.Dodatak)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["DodatakId"] = new SelectList(_context.Dodatak, "Id", "Id");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Kategorija,Naziv,DodatakId")] Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proizvod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DodatakId"] = new SelectList(_context.Dodatak, "Id", "Id", proizvod.DodatakId);
            return View(proizvod);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod == null)
            {
                return NotFound();
            }
            ViewData["DodatakId"] = new SelectList(_context.Dodatak, "Id", "Id", proizvod.DodatakId);
            return View(proizvod);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Kategorija,Naziv,DodatakId")] Proizvod proizvod)
        {
            if (id != proizvod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProizvodExists(proizvod.Id))
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
            ViewData["DodatakId"] = new SelectList(_context.Dodatak, "Id", "Id", proizvod.DodatakId);
            return View(proizvod);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvod = await _context.Proizvod
                .Include(p => p.Dodatak)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }


        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvod = await _context.Proizvod.FindAsync(id);
            if (proizvod != null)
            {
                _context.Proizvod.Remove(proizvod);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProizvodExists(int id)
        {
            return _context.Proizvod.Any(e => e.Id == id);
        }
    }
}
