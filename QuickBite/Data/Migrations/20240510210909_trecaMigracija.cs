using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBite.Data.Migrations
{
    /// <inheritdoc />
    public partial class trecaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UsluznaJedinica_ProizvodId",
                table: "UsluznaJedinica",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodNarudzba_NarudzbaId",
                table: "ProizvodNarudzba",
                column: "NarudzbaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProizvodNarudzba_ProizvodId",
                table: "ProizvodNarudzba",
                column: "ProizvodId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_KupacId",
                table: "Narudzba",
                column: "KupacId");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_NaplataId",
                table: "Narudzba",
                column: "NaplataId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KupacId",
                table: "Narudzba",
                column: "KupacId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Naplata_NaplataId",
                table: "Narudzba",
                column: "NaplataId",
                principalTable: "Naplata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodNarudzba_Narudzba_NarudzbaId",
                table: "ProizvodNarudzba",
                column: "NarudzbaId",
                principalTable: "Narudzba",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProizvodNarudzba_Proizvod_ProizvodId",
                table: "ProizvodNarudzba",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_KupacId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Naplata_NaplataId",
                table: "Narudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodNarudzba_Narudzba_NarudzbaId",
                table: "ProizvodNarudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_ProizvodNarudzba_Proizvod_ProizvodId",
                table: "ProizvodNarudzba");

            migrationBuilder.DropForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.DropIndex(
                name: "IX_UsluznaJedinica_ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.DropIndex(
                name: "IX_ProizvodNarudzba_NarudzbaId",
                table: "ProizvodNarudzba");

            migrationBuilder.DropIndex(
                name: "IX_ProizvodNarudzba_ProizvodId",
                table: "ProizvodNarudzba");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_KupacId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_NaplataId",
                table: "Narudzba");
        }
    }
}
