using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AkilliMesaiPlanlayici.Migrations
{
    /// <inheritdoc />
    public partial class InitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personeller",
                columns: table => new
                {
                    PersonelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SicilNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Durum = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personeller", x => x.PersonelID);
                });

            migrationBuilder.CreateTable(
                name: "Vardiyalar",
                columns: table => new
                {
                    VardiyaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VardiyaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaslangicSaati = table.Column<TimeSpan>(type: "time", nullable: false),
                    BitisSaati = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vardiyalar", x => x.VardiyaID);
                });

            migrationBuilder.CreateTable(
                name: "FazlaMesaier",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaatSayisi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Neden = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnayDurumu = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FazlaMesaier", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FazlaMesaier_Personeller_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personeller",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonelVardiyalar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelID = table.Column<int>(type: "int", nullable: false),
                    VardiyaID = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GercekBaslangic = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GercekBitis = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelVardiyalar", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonelVardiyalar_Personeller_PersonelID",
                        column: x => x.PersonelID,
                        principalTable: "Personeller",
                        principalColumn: "PersonelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonelVardiyalar_Vardiyalar_VardiyaID",
                        column: x => x.VardiyaID,
                        principalTable: "Vardiyalar",
                        principalColumn: "VardiyaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FazlaMesaier_PersonelID",
                table: "FazlaMesaier",
                column: "PersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelVardiyalar_PersonelID",
                table: "PersonelVardiyalar",
                column: "PersonelID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonelVardiyalar_VardiyaID",
                table: "PersonelVardiyalar",
                column: "VardiyaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FazlaMesaier");

            migrationBuilder.DropTable(
                name: "PersonelVardiyalar");

            migrationBuilder.DropTable(
                name: "Personeller");

            migrationBuilder.DropTable(
                name: "Vardiyalar");
        }
    }
}
