using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBite.Data.Migrations
{
    /// <inheritdoc />
    public partial class drugaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "UsluznaJedinica",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "proizvod_id",
                table: "UsluznaJedinica",
                newName: "TipUsluge");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ProizvodNarudzba",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "proizvod_id",
                table: "ProizvodNarudzba",
                newName: "ProizvodId");

            migrationBuilder.RenameColumn(
                name: "narudzba_id",
                table: "ProizvodNarudzba",
                newName: "NarudzbaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Proizvod",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "dodatak_id",
                table: "Proizvod",
                newName: "Kategorija");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Narudzba",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "naplata_id",
                table: "Narudzba",
                newName: "VrijemeNarudzbe");

            migrationBuilder.RenameColumn(
                name: "kupac_id",
                table: "Narudzba",
                newName: "NaplataId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Naplata",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Korisnik",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Dodatak",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "UsluznaJedinica",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "UsluznaJedinica",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProizvodId",
                table: "UsluznaJedinica",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DodatakId",
                table: "Proizvod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Naziv",
                table: "Proizvod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Cijena",
                table: "Narudzba",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KupacId",
                table: "Narudzba",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BrojKartice",
                table: "Naplata",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Napomena",
                table: "Naplata",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VrstaNaplate",
                table: "Naplata",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Adresa",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "BrojNarudzbi",
                table: "Korisnik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BrojTelefona",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OstvareneNarudzbe",
                table: "Korisnik",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "TrenutnoZauzet",
                table: "Korisnik",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Korisnik",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PriborZaJelo",
                table: "Dodatak",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Salata",
                table: "Dodatak",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sos",
                table: "Dodatak",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Zacin",
                table: "Dodatak",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_DodatakId",
                table: "Proizvod",
                column: "DodatakId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvod_Dodatak_DodatakId",
                table: "Proizvod",
                column: "DodatakId",
                principalTable: "Dodatak",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proizvod_Dodatak_DodatakId",
                table: "Proizvod");

            migrationBuilder.DropIndex(
                name: "IX_Proizvod_DodatakId",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "UsluznaJedinica");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "UsluznaJedinica");

            migrationBuilder.DropColumn(
                name: "ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.DropColumn(
                name: "DodatakId",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Naziv",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Cijena",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "KupacId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "BrojKartice",
                table: "Naplata");

            migrationBuilder.DropColumn(
                name: "Napomena",
                table: "Naplata");

            migrationBuilder.DropColumn(
                name: "VrstaNaplate",
                table: "Naplata");

            migrationBuilder.DropColumn(
                name: "Adresa",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "BrojNarudzbi",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "BrojTelefona",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "OstvareneNarudzbe",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "TrenutnoZauzet",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Korisnik");

            migrationBuilder.DropColumn(
                name: "PriborZaJelo",
                table: "Dodatak");

            migrationBuilder.DropColumn(
                name: "Salata",
                table: "Dodatak");

            migrationBuilder.DropColumn(
                name: "Sos",
                table: "Dodatak");

            migrationBuilder.DropColumn(
                name: "Zacin",
                table: "Dodatak");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UsluznaJedinica",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "TipUsluge",
                table: "UsluznaJedinica",
                newName: "proizvod_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProizvodNarudzba",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ProizvodId",
                table: "ProizvodNarudzba",
                newName: "proizvod_id");

            migrationBuilder.RenameColumn(
                name: "NarudzbaId",
                table: "ProizvodNarudzba",
                newName: "narudzba_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Proizvod",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Kategorija",
                table: "Proizvod",
                newName: "dodatak_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Narudzba",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "VrijemeNarudzbe",
                table: "Narudzba",
                newName: "naplata_id");

            migrationBuilder.RenameColumn(
                name: "NaplataId",
                table: "Narudzba",
                newName: "kupac_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Naplata",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Korisnik",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dodatak",
                newName: "id");
        }
    }
}
