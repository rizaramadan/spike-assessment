using Microsoft.EntityFrameworkCore.Migrations;

namespace Assessment.Migrations
{
    public partial class soal_id_to_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoalId",
                table: "Soal",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Soal",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Soal");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Soal",
                newName: "SoalId");
        }
    }
}
