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
            var applicationDbContext = _context.UsluznaJedinica.Include(u => u.Proizvodi);
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
                .Include(u => u.Proizvodi)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usluznaJedinica == null)
            {
                return NotFound();
            }

            return View(usluznaJedinica);
        }

        private void NapuniBazuPodataka()
        {
            //Dodaci
            var dodaci = new List<Dodatak>
            {
                //prvi paket
                new Dodatak { PriborZaJelo = true, Zacin = Zacin.So, Sos = Sos.Kecap, Salata = Salata.Kupus },
                //drugi paket
                new Dodatak { PriborZaJelo = false, Zacin = Zacin.Biber, Sos = Sos.Majoneza, Salata = Salata.Paradajz },
                //treci paket
                new Dodatak { PriborZaJelo = true, Zacin = Zacin.Cili, Sos = Sos.Senf, Salata = Salata.Krastavice },
                //cetvrti paket
                new Dodatak { PriborZaJelo = false, Zacin = Zacin.Cili, Sos = Sos.Senf, Salata = Salata.Krastavice },
                //peti paket
                new Dodatak { PriborZaJelo = true, Zacin = Zacin.Cili, Sos = Sos.Senf, Salata = Salata.Krastavice },
                //sesti paket
                new Dodatak { PriborZaJelo = false, Zacin = Zacin.Cili, Sos = Sos.Senf, Salata = Salata.Krastavice },
            };
            _context.Dodatak.AddRange(dodaci);
            _context.SaveChanges();

            var places = new List<UsluznaJedinica>
            {
                // Restorani
                new UsluznaJedinica { TipUsluge = Kategorija.Restorani, Naziv = "Arigato-sushi bar", Adresa = "Čobanija 1, Sarajevo 71000" },
                new UsluznaJedinica { TipUsluge = Kategorija.Restorani, Naziv = "Metropolis", Adresa = "Maršala Tita 21, Sarajevo 71000" },
                new UsluznaJedinica { TipUsluge = Kategorija.Restorani, Naziv = "Ćevabdžinica Hodžić", Adresa = "Bravadžiluk 34, Sarajevo 71000" },

                // Supermarketi
                new UsluznaJedinica { TipUsluge = Kategorija.Supermarketi, Naziv = "Amko Komerc", Adresa = "Butmirska cesta 20, Ilidža" },
                new UsluznaJedinica { TipUsluge = Kategorija.Supermarketi, Naziv = "Merkator", Adresa = "Ložionička 16, Sarajevo 71000" },
                new UsluznaJedinica { TipUsluge = Kategorija.Supermarketi, Naziv = "Bingo", Adresa = "Dzemala Bijedica St 160, Sarajevo 71000" },

                // Brza Hrana
                new UsluznaJedinica { TipUsluge = Kategorija.BrzaHrana, Naziv = "KFC", Adresa = "Vrbanja 1, Sarajevo 71000" },
                new UsluznaJedinica { TipUsluge = Kategorija.BrzaHrana, Naziv = "Bash", Adresa = "Kulovića br.5, Sarajevo 71000" },
                new UsluznaJedinica { TipUsluge = Kategorija.BrzaHrana, Naziv = "Burger King", Adresa = "Vrbanja 1, Sarajevo 71000" }
            };

            _context.UsluznaJedinica.AddRange(places);
            _context.SaveChanges();

            // Kreiraj proizvode i dodaj im dodatke
            var proizvodi = new List<Proizvod>
            {
                //Metropolis
                new Proizvod { Naziv = "Gringas", Cijena=10, Kategorija = Kategorija.Restorani, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Metropolis").Id },
                new Proizvod { Naziv = "American Pizza", Cijena=12, Kategorija = Kategorija.Restorani, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Metropolis").Id },
                new Proizvod { Naziv = "American Cheeseburger", Cijena=8, Kategorija = Kategorija.Restorani, DodatakId = dodaci[3].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Metropolis").Id },
                new Proizvod { Naziv = "Whitella", Cijena=4,Kategorija = Kategorija.Restorani, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Metropolis").Id },
                new Proizvod { Naziv = "Monte torta",Cijena=5, Kategorija = Kategorija.Restorani, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Metropolis").Id },
                new Proizvod { Naziv = "Kraljica", Cijena=4,Kategorija = Kategorija.Restorani, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Metropolis").Id },
                //Hodzic
                new Proizvod { Naziv = "Ćevapi", Cijena=10, Kategorija = Kategorija.Restorani, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Ćevabdžinica Hodžić").Id },
                new Proizvod { Naziv = "Pljeskavica", Cijena=8, Kategorija = Kategorija.Restorani, DodatakId = dodaci[4].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Ćevabdžinica Hodžić").Id },
                new Proizvod { Naziv = "Kombinacija", Cijena = 15, Kategorija = Kategorija.Restorani, DodatakId = dodaci[2].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Ćevabdžinica Hodžić").Id },
                new Proizvod { Naziv = "Šnicla", Cijena = 6, Kategorija = Kategorija.Restorani, DodatakId = dodaci[3].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Ćevabdžinica Hodžić").Id },
                new Proizvod { Naziv = "Kotleti", Cijena = 8, Kategorija = Kategorija.Restorani, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Ćevabdžinica Hodžić").Id },
                //Arigato
                new Proizvod { Naziv = "Sashimi", Cijena = 15, Kategorija = Kategorija.Restorani, DodatakId = dodaci[1].Id,  UsluznaJedinicaId = places.First(p => p.Naziv == "Arigato-sushi bar").Id },
                new Proizvod { Naziv = "Tataki tuna", Cijena = 20, Kategorija = Kategorija.Restorani, DodatakId = dodaci[2].Id,  UsluznaJedinicaId = places.First(p => p.Naziv == "Arigato-sushi bar").Id },
                new Proizvod { Naziv = "Panko losos", Cijena = 12, Kategorija = Kategorija.Restorani, DodatakId = dodaci[4].Id,  UsluznaJedinicaId = places.First(p => p.Naziv == "Arigato-sushi bar").Id },
                new Proizvod { Naziv = "Mango roll", Cijena = 12, Kategorija = Kategorija.Restorani, DodatakId = dodaci[1].Id,  UsluznaJedinicaId = places.First(p => p.Naziv == "Arigato-sushi bar").Id },
                new Proizvod { Naziv = "Nigiri losos", Cijena = 20, Kategorija = Kategorija.Restorani, DodatakId = dodaci[3].Id,  UsluznaJedinicaId = places.First(p => p.Naziv == "Arigato-sushi bar").Id },
                //Merkator
                new Proizvod { Naziv = "Riza", Cijena=2, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id,  UsluznaJedinicaId = places.First(p => p.Naziv == "Merkator").Id },
                new Proizvod { Naziv = "Jaja", Cijena = 3, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Merkator").Id },
                new Proizvod { Naziv = "So", Cijena = 2, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Merkator").Id },
                new Proizvod { Naziv = "Brasno", Cijena = 7, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Merkator").Id },
                new Proizvod { Naziv = "Jufka", Cijena=3, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Merkator").Id },
                //Bingo
                new Proizvod { Naziv = "Deterdzent", Cijena=6, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bingo").Id },
                new Proizvod { Naziv = "Kaladont", Cijena=3, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bingo").Id },
                new Proizvod { Naziv = "Maslac", Cijena=8, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bingo").Id },
                new Proizvod { Naziv = "Banane", Cijena=2, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bingo").Id },
                new Proizvod { Naziv = "Jabuke", Cijena=1, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bingo").Id },
                //Amko
                new Proizvod { Naziv = "Hljeb", Cijena=2, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Amko Komerc").Id },
                new Proizvod { Naziv = "Kobasice", Cijena=3, Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Amko Komerc").Id },
                new Proizvod { Naziv = "Salama", Cijena=2,Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Amko Komerc").Id },
                new Proizvod { Naziv = "Supa", Cijena=1,Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Amko Komerc").Id },
                new Proizvod { Naziv = "Makarone", Cijena=2,Kategorija = Kategorija.Supermarketi, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Amko Komerc").Id },
                //Bash
                new Proizvod { Naziv = "Hamburger", Cijena=5, Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[3].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bash").Id },
                new Proizvod { Naziv = "Cheeseburger", Cijena=6, Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bash").Id },
                new Proizvod { Naziv = "Master Burger", Cijena=8,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[4].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bash").Id },
                new Proizvod { Naziv = "Nuggets", Cijena=6,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bash").Id },
                new Proizvod { Naziv = "Doner", Cijena=7,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[2].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Bash").Id },
                //KFC
                new Proizvod { Naziv = "Zinger meni", Cijena=10,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "KFC").Id },
                new Proizvod { Naziv = "Twister meni", Cijena=10,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[2].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "KFC").Id  },
                new Proizvod { Naziv = "Twister box", Cijena=12,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[3].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "KFC").Id  },
                new Proizvod { Naziv = "Twister", Cijena=7,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[4].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "KFC").Id  },
                new Proizvod { Naziv = "Boxmaster", Cijena=15,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "KFC").Id  },
                //BurgerKing
                new Proizvod { Naziv = "Big King", Cijena=12,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Burger King").Id },
                new Proizvod { Naziv = "Texas Burger", Cijena=8,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[1].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Burger King").Id },
                new Proizvod { Naziv = "Pileća krilca", Cijena=6,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[2].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Burger King").Id },
                new Proizvod { Naziv = "Veliki pomfrit", Cijena=5,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[3].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Burger King").Id },
                new Proizvod { Naziv = "Onion Rings", Cijena=5,Kategorija = Kategorija.BrzaHrana, DodatakId = dodaci[0].Id, UsluznaJedinicaId = places.First(p => p.Naziv == "Burger King").Id }
                // Dodaj još proizvoda po potrebi
            };

            _context.Proizvod.AddRange(proizvodi);
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
        public async Task<IActionResult> Create([Bind("Id,TipUsluge,Naziv,Adresa")] UsluznaJedinica usluznaJedinica)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usluznaJedinica);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(usluznaJedinica);
        }

        // POST: Place/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                .Include(u => u.Proizvodi)
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
