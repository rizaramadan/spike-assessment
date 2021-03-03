using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Assessment.Migrations
{
    public partial class paket_soal_id_to_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soal_PaketSoal_PaketSoalId",
                table: "Soal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaketSoal",
                table: "PaketSoal");

            migrationBuilder.RenameColumn(
                name: "PaketSoalId",
                table: "PaketSoal",
                newName: "UpdatorId");

            migrationBuilder.AddColumn<int>(
                name: "No",
                table: "Soal",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "UpdatorId",
                table: "PaketSoal",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "PaketSoal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "PaketSoal",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "PaketSoal",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PaketSoal",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "PaketSoal",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaketSoal",
                table: "PaketSoal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Soal_PaketSoal_PaketSoalId",
                table: "Soal",
                column: "PaketSoalId",
                principalTable: "PaketSoal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Soal_PaketSoal_PaketSoalId",
                table: "Soal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaketSoal",
                table: "PaketSoal");

            migrationBuilder.DropColumn(
                name: "No",
                table: "Soal");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PaketSoal");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "PaketSoal");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "PaketSoal");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PaketSoal");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "PaketSoal");

            migrationBuilder.RenameColumn(
                name: "UpdatorId",
                table: "PaketSoal",
                newName: "PaketSoalId");

            migrationBuilder.AlterColumn<long>(
                name: "PaketSoalId",
                table: "PaketSoal",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaketSoal",
                table: "PaketSoal",
                column: "PaketSoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Soal_PaketSoal_PaketSoalId",
                table: "Soal",
                column: "PaketSoalId",
                principalTable: "PaketSoal",
                principalColumn: "PaketSoalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
