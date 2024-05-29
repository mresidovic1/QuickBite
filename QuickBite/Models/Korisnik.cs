using Microsoft.AspNetCore.Identity;

namespace QuickBite.Models
{
    public class Korisnik : IdentityUser
    {
        public string? Adresa { get; set; }
        public int? BrojNarudzbi { get; set; }
        public int? OstvareneNarudzbe { get; set; }
        public Boolean? TrenutnoZauzet { get; set; }
        public ICollection<Narudzba>? Narudzbe { get; set; }
    }
}
