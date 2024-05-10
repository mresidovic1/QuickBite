using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuickBite.Models
{
    public class ProizvodNarudzba
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Proizvod")]
        public int ProizvodId { get; set; }

        [ForeignKey("Narudzba")]
        public int NarudzbaId { get; set; }
        public Narudzba Narudzba { get; set; }
        public Proizvod Proizvod { get; set; }
    }
}
