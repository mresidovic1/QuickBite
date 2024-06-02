using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBite.Data.Migrations
{
    /// <inheritdoc />
    public partial class osmaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.DropIndex(
                name: "IX_UsluznaJedinica_ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.DropColumn(
                name: "ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.AddColumn<int>(
                name: "UsluznaJedinicaId",
                table: "Proizvod",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Proizvod_UsluznaJedinicaId",
                table: "Proizvod",
                column: "UsluznaJedinicaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proizvod_UsluznaJedinica_UsluznaJedinicaId",
                table: "Proizvod",
                column: "UsluznaJedinicaId",
                principalTable: "UsluznaJedinica",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proizvod_UsluznaJedinica_UsluznaJedinicaId",
                table: "Proizvod");

            migrationBuilder.DropIndex(
                name: "IX_Proizvod_UsluznaJedinicaId",
                table: "Proizvod");

            migrationBuilder.DropColumn(
                name: "UsluznaJedinicaId",
                table: "Proizvod");

            migrationBuilder.AddColumn<int>(
                name: "ProizvodId",
                table: "UsluznaJedinica",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsluznaJedinica_ProizvodId",
                table: "UsluznaJedinica",
                column: "ProizvodId");

            migrationBuilder.AddForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id");
        }
    }
}
