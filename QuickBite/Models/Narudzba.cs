using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuickBite.Models
{
    public class Narudzba
    {
        [Key]
        public int Id { get; set; }
        public int Cijena { get; set; }
        public int? Popust { get; set; }
        public int VrijemeNarudzbe { get; set; }
        [ForeignKey("UsluznaJedinica")]
        public int UsluznaJedinicaId { get; set; }
        public UsluznaJedinica UsluznaJedinica { get; set; }

        [ForeignKey("Naplata")]
        public int NaplataId { get; set; }
        public Naplata Naplata { get; set; }

        [ForeignKey("Korisnik")]
        public string KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
    }
}

