﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickBite.Models;

namespace QuickBite.Data
{
    public class ApplicationDbContext : IdentityDbContext<Korisnik>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Dodatak> Dodatak { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Naplata> Naplata { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<ProizvodNarudzba> ProizvodNarudzba { get; set; }
        public DbSet<UsluznaJedinica> UsluznaJedinica { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dodatak>().ToTable("Dodatak");
            modelBuilder.Entity<Naplata>().ToTable("Naplata");
            modelBuilder.Entity<Narudzba>().ToTable("Narudzba");
            modelBuilder.Entity<Proizvod>().ToTable("Proizvod");
            modelBuilder.Entity<ProizvodNarudzba>().ToTable("ProizvodNarudzba");
            modelBuilder.Entity<UsluznaJedinica>().ToTable("UsluznaJedinica");
            modelBuilder.Entity<Narudzba>()
                .HasOne(n => n.Korisnik)
                .WithMany(k => k.Narudzbe)
                .HasForeignKey(n => n.KorisnikId);
            modelBuilder.Entity<Korisnik>(b =>
            {
                b.Property(u => u.Adresa);
                b.Property(u => u.BrojNarudzbi);
                b.Property(u => u.OstvareneNarudzbe);
                b.Property(u => u.TrenutnoZauzet);
                base.OnModelCreating(modelBuilder);
            });
            modelBuilder.Entity<UsluznaJedinica>()
                .HasMany(u => u.Proizvodi)
                .WithOne()
                .HasForeignKey(p => p.UsluznaJedinicaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
