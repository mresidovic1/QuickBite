using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuickBite.Models
{
    public class Proizvod
    {
        [Key]
        public int Id { get; set; }
        public Dodatak Dodatak { get; set; }
        public Kategorija Kategorija { get; set; }
        public string Naziv { get; set; }

        [ForeignKey("Dodatak")]
        public int DodatakId { get; set; }
    }
}
