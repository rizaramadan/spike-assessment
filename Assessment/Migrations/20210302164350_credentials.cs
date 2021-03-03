using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Assessment.Migrations
{
    public partial class credentials : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "JawabanIntakes");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "JawabanIntakes");

            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IntakeId = table.Column<long>(type: "bigint", nullable: false),
                    SiswaId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Credential_Intakes_IntakeId",
                        column: x => x.IntakeId,
                        principalTable: "Intakes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Credential_Siswa_SiswaId",
                        column: x => x.SiswaId,
                        principalTable: "Siswa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JawabanIntakes_SoalId",
                table: "JawabanIntakes",
                column: "SoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Credential_IntakeId",
                table: "Credential",
                column: "IntakeId");

            migrationBuilder.CreateIndex(
                name: "IX_Credential_SiswaId",
                table: "Credential",
                column: "SiswaId");

            migrationBuilder.AddForeignKey(
                name: "FK_JawabanIntakes_Soal_SoalId",
                table: "JawabanIntakes",
                column: "SoalId",
                principalTable: "Soal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JawabanIntakes_Soal_SoalId",
                table: "JawabanIntakes");

            migrationBuilder.DropTable(
                name: "Credential");

            migrationBuilder.DropIndex(
                name: "IX_JawabanIntakes_SoalId",
                table: "JawabanIntakes");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "JawabanIntakes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "JawabanIntakes",
                type: "text",
                nullable: true);
        }
    }
}
