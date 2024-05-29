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
            migrationBuilder.DropForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.AlterColumn<int>(
                name: "ProizvodId",
                table: "UsluznaJedinica",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica");

            migrationBuilder.AlterColumn<int>(
                name: "ProizvodId",
                table: "UsluznaJedinica",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UsluznaJedinica_Proizvod_ProizvodId",
                table: "UsluznaJedinica",
                column: "ProizvodId",
                principalTable: "Proizvod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
