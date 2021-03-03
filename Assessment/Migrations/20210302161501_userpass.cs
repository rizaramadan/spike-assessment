using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Migrations
{
    public partial class userpass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "JawabanIntakes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SiswaId",
                table: "JawabanIntakes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "JawabanIntakes",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JawabanIntakes_SiswaId",
                table: "JawabanIntakes",
                column: "SiswaId");

            migrationBuilder.AddForeignKey(
                name: "FK_JawabanIntakes_Siswa_SiswaId",
                table: "JawabanIntakes",
                column: "SiswaId",
                principalTable: "Siswa",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JawabanIntakes_Siswa_SiswaId",
                table: "JawabanIntakes");

            migrationBuilder.DropIndex(
                name: "IX_JawabanIntakes_SiswaId",
                table: "JawabanIntakes");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "JawabanIntakes");

            migrationBuilder.DropColumn(
                name: "SiswaId",
                table: "JawabanIntakes");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "JawabanIntakes");
        }
    }
}
