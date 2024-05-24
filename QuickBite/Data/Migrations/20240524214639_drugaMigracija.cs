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
            migrationBuilder.AddColumn<string>(
                name: "KorisnikId",
                table: "Narudzba",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_KorisnikId",
                table: "Narudzba",
                column: "KorisnikId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikId",
                table: "Narudzba",
                column: "KorisnikId",
                principalTable: "Korisnik",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_Korisnik_KorisnikId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_KorisnikId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "KorisnikId",
                table: "Narudzba");
        }
    }
}
