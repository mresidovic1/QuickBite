using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBite.Data.Migrations
{
    /// <inheritdoc />
    public partial class petaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsluznaJedinicaId",
                table: "Narudzba",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Narudzba_UsluznaJedinicaId",
                table: "Narudzba",
                column: "UsluznaJedinicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Narudzba_UsluznaJedinica_UsluznaJedinicaId",
                table: "Narudzba",
                column: "UsluznaJedinicaId",
                principalTable: "UsluznaJedinica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Narudzba_UsluznaJedinica_UsluznaJedinicaId",
                table: "Narudzba");

            migrationBuilder.DropIndex(
                name: "IX_Narudzba_UsluznaJedinicaId",
                table: "Narudzba");

            migrationBuilder.DropColumn(
                name: "UsluznaJedinicaId",
                table: "Narudzba");
        }
    }
}
