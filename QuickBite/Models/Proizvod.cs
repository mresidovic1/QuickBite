using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBite.Models
{
    public class Proizvod
    {
        [Key]
        public int Id { get; set; }
        public Kategorija Kategorija { get; set; }
        public string Naziv { get; set; }
        public int? Cijena { get; set; }
        public Dodatak Dodatak { get; set; }

        [ForeignKey("Dodatak")]
        public int DodatakId { get; set; }

        [ForeignKey("UsluznaJedinica")]
        public int UsluznaJedinicaId { get; set; }
    }
}
