using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindApi.Migrations
{
    public partial class RemovedFullNamecolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Persons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
