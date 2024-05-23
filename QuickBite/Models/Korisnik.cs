using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace QuickBite.Models
{
    public class Korisnik
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string BrojTelefona { get; set; }
        public int BrojNarudzbi { get; set; }
        public int OstvareneNarudzbe { get; set; }
        public Boolean TrenutnoZauzet { get; set; }
    }
}
