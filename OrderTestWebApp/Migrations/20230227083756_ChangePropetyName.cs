using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderTestWebApp.Migrations
{
    public partial class ChangePropetyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Orders",
                newName: "OrderType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "Orders",
                newName: "Type");
        }
    }
}
