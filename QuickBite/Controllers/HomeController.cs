using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickBite.Data;
using QuickBite.Models;
using System;
using System.Diagnostics;

namespace QuickBite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<Korisnik> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<Korisnik> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context; // Dodajte kontekst baze podataka
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult MapView()
        {
            return View();
        }
        public IActionResult RegisterConfirmation()
        {
            return View();
        }
        public IActionResult Kosarica()
        {
            return RedirectToAction("Index", "Order");
        }
        public IActionResult Kurir()
        {
            return RedirectToAction("Index", "Kurir");
        }
        public IActionResult Admin()
        {
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public async Task<IActionResult> PotvrdiLokaciju(string location)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Postavite adresu korisnika na unesenu adresu
            currentUser.Adresa = location;

            // Ažurirajte korisnika u bazi podataka
            await _userManager.UpdateAsync(currentUser);

            // Preusmjeri se na neki drugi pogled ili vrati neki rezultat
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var foundPlace = await _context.UsluznaJedinica
                .FirstOrDefaultAsync(p => p.Naziv.Contains(searchTerm));

            if (foundPlace != null)
            {
                return RedirectToAction("Index", "Items", new { usluznaJedinicaId = foundPlace.Id });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

    }
}

        
