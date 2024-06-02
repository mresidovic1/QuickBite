using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBite.Models
{
    public class UsluznaJedinica
    {
        [Key]
        public int Id { get; set; }
        public Kategorija TipUsluge { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }

        public ICollection<Proizvod> Proizvodi { get; set; } = new List<Proizvod>();
    }
}
