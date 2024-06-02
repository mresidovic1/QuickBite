﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickBite.Data.Migrations
{
    /// <inheritdoc />
    public partial class desetaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cijena",
                table: "Proizvod",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Cijena",
                table: "Proizvod",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
