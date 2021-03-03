using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Assessment.Migrations
{
    public partial class intakes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Kunci",
                table: "Soal",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Intakes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Tipe = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RombonganId = table.Column<long>(type: "bigint", nullable: false),
                    PaketSoalId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intakes_PaketSoal_PaketSoalId",
                        column: x => x.PaketSoalId,
                        principalTable: "PaketSoal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Intakes_Rombels_RombonganId",
                        column: x => x.RombonganId,
                        principalTable: "Rombels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JawabanIntakes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IntakeId = table.Column<long>(type: "bigint", nullable: false),
                    SoalId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Jawaban = table.Column<string>(type: "text", nullable: true),
                    Skor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JawabanIntakes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JawabanIntakes_Intakes_IntakeId",
                        column: x => x.IntakeId,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_PaketSoalId",
                table: "Intakes",
                column: "PaketSoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Intakes_RombonganId",
                table: "Intakes",
                column: "RombonganId");

            migrationBuilder.CreateIndex(
                name: "IX_JawabanIntakes_IntakeId",
                table: "JawabanIntakes",
                column: "IntakeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JawabanIntakes");

            migrationBuilder.DropTable(
                name: "Intakes");

            migrationBuilder.DropColumn(
                name: "Kunci",
                table: "Soal");
        }
    }
}
