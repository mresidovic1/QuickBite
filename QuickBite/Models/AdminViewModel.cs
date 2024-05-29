namespace QuickBite.Models
{
    public class AdminViewModel
    {
        public IEnumerable<UsluznaJedinica> UsluzneJedinice { get; set;}
        public IEnumerable<Korisnik> Korisnici { get; set;}
        public IEnumerable<Korisnik> Kuriri {  get; set;}
    }
}
