using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkilliMesaiPlanlayici.Migrations
{
    /// <inheritdoc />
    public partial class AddSaatlikUcretColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PersonelVardiyalar",
                newName: "PersonelVardiyaID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GercekBaslangic",
                table: "PersonelVardiyalar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SaatlikUcret",
                table: "Personeller",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaatlikUcret",
                table: "Personeller");

            migrationBuilder.RenameColumn(
                name: "PersonelVardiyaID",
                table: "PersonelVardiyalar",
                newName: "ID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "GercekBaslangic",
                table: "PersonelVardiyalar",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
