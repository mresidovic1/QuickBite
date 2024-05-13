using System.ComponentModel.DataAnnotations;

namespace QuickBite.Models
{
    public class Dodatak
    {
        [Key]
        public int Id { get; set; }
        public Boolean PriborZaJelo { get; set; }
        public Zacin Zacin { get; set; }
        public Sos Sos { get; set; }
        public Salata Salata { get; set; }
    }
}
