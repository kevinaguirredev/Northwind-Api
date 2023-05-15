using Microsoft.EntityFrameworkCore.Migrations;

namespace NorthwindApi.Migrations
{
    public partial class AddedPersonTypeCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PersonType",
                table: "Persons",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonType",
                table: "Persons");
        }
    }
}
