using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBite.Data.Migrations
{
    /// <inheritdoc />
    public partial class migracijaA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Kolicina",
                table: "Proizvod",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Popust",
                table: "Narudzba",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Kolicina",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "Popust",
                table: "Narudzba");
        }
    }
}
