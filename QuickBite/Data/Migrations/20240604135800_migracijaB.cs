using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBite.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracijaB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolicina",
                table: "Proizvod");

            migrationBuilder.AddColumn<int>(
                name: "Kolicina",
                table: "ProizvodNarudzba",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolicina",
                table: "ProizvodNarudzba");

            migrationBuilder.AddColumn<int>(
                name: "Kolicina",
                table: "Proizvod",
                type: "int",
                nullable: true);
        }
    }
}
