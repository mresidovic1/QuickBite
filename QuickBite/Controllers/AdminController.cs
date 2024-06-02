using System;
using System.Collections.Generic;
using System.Dynamic;
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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Korisnik> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<Korisnik> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            AdminViewModel mymodel = new AdminViewModel
            {
                UsluzneJedinice = await GetUsluzneJedinice(),
                Korisnici = await GetKorisnici(),
                Kuriri = await GetUsersByRole("Kurir")
            };
            return View(mymodel);
        }

        private async Task<IEnumerable<UsluznaJedinica>> GetUsluzneJedinice()
        {
            return await _context.UsluznaJedinica.Include(u => u.Proizvodi).ToListAsync();
        }

        private async Task<IEnumerable<Korisnik>> GetUsersByRole(string role)
        {
            var users = await _userManager.GetUsersInRoleAsync(role);
            return users;
        }

        private async Task<IEnumerable<Korisnik>> GetKorisnici()
        {
            var allUsers = await _context.Korisnik.ToListAsync();

            var kuriri = await GetUsersByRole("Kurir");

            var administratori = await GetUsersByRole("Admin");

            var korisnici = allUsers.Where(u => !kuriri.Contains(u) && !administratori.Contains(u));

            return korisnici;
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluznaJedinica = await _context.UsluznaJedinica
                .Include(u => u.Proizvodi).ToListAsync();
            if (usluznaJedinica == null)
            {
                return NotFound();
            }

            return View(usluznaJedinica);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            ViewData["ProizvodId"] = new SelectList(_context.Proizvod, "Id", "Id");
            return View();
        }

        // POST: Admin/Create
        // POST: Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipUsluge,Naziv,Adresa")] UsluznaJedinica usluznaJedinica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usluznaJedinica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }
            return View(usluznaJedinica);
        }

        // GET: Admin/Edit/5
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
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipUsluge,Naziv,Adresa")] UsluznaJedinica usluznaJedinica)
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
            return View();
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usluznaJedinica = await _context.UsluznaJedinica.FirstOrDefaultAsync(m => m.Id == id);
            if (usluznaJedinica == null)
            {
                return NotFound();
            }

            return View(usluznaJedinica);
        }

        

        // POST: Admin/Delete/5
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

        // GET: Admin/DeleteCourier/5
        public async Task<IActionResult> DeleteCourier(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courier = await _userManager.FindByIdAsync(id);
            if (courier == null)
            {
                return NotFound();
            }

            return View(courier);
        }

        // POST: Admin/DeleteCourier/5
        [HttpPost, ActionName("DeleteCourier")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourierConfirmed(string id)
        {
            var courier = await _userManager.FindByIdAsync(id);
            if (courier != null)
            {
                var result = await _userManager.DeleteAsync(courier);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
