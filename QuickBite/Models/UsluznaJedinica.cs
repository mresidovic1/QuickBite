using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuickBite.Models
{
    public class UsluznaJedinica
    {
        [Key]
        public int Id { get; set; }
        public Kategorija TipUsluge { get; set; }
        public string Naziv { get; set; }
        public string Adresa { get; set; }

        [ForeignKey("Proizvod")]
        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
