using System.ComponentModel.DataAnnotations;

namespace QuickBite.Models
{
    public class Naplata
    {
        [Key]
        public int Id { get; set; }
        public VrstaNaplate VrstaNaplate { get; set; }
        public int BrojKartice { get; set; }
        public string Napomena { get; set; }
    }
}
