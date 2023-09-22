using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_User_UserId1",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_UserId1",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CNPJ",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CPF",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserDesativado",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Department");

            migrationBuilder.AddColumn<string>(
                name: "Documento",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Documento",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "CNPJ",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPF",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "UserDesativado",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Department",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_UserId1",
                table: "Department",
                column: "UserId1",
                unique: true,
                filter: "[UserId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_User_UserId1",
                table: "Department",
                column: "UserId1",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
