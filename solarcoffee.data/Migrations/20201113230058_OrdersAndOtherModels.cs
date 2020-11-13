using Microsoft.EntityFrameworkCore.Migrations;

namespace solarcoffee.data.Migrations
{
    public partial class OrdersAndOtherModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "ProductInventories",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "ProductInventories");
        }
    }
}
