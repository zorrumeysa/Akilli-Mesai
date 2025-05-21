using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkilliMesaiPlanlayici.Migrations
{
    /// <inheritdoc />
    public partial class AddMeslekAndMaasToPersonel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SaatlikUcret",
                table: "Personeller",
                newName: "AylikMaas");

            migrationBuilder.AddColumn<int>(
                name: "MeslekID",
                table: "Personeller",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Neden",
                table: "FazlaMesaier",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GercekBaslangic",
                table: "FazlaMesaier",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "GercekBitis",
                table: "FazlaMesaier",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Meslek",
                columns: table => new
                {
                    MeslekID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrtalamaMaas = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meslek", x => x.MeslekID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personeller_MeslekID",
                table: "Personeller",
                column: "MeslekID");

            migrationBuilder.AddForeignKey(
                name: "FK_Personeller_Meslek_MeslekID",
                table: "Personeller",
                column: "MeslekID",
                principalTable: "Meslek",
                principalColumn: "MeslekID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personeller_Meslek_MeslekID",
                table: "Personeller");

            migrationBuilder.DropTable(
                name: "Meslek");

            migrationBuilder.DropIndex(
                name: "IX_Personeller_MeslekID",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "MeslekID",
                table: "Personeller");

            migrationBuilder.DropColumn(
                name: "GercekBaslangic",
                table: "FazlaMesaier");

            migrationBuilder.DropColumn(
                name: "GercekBitis",
                table: "FazlaMesaier");

            migrationBuilder.RenameColumn(
                name: "AylikMaas",
                table: "Personeller",
                newName: "SaatlikUcret");

            migrationBuilder.AlterColumn<string>(
                name: "Neden",
                table: "FazlaMesaier",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
