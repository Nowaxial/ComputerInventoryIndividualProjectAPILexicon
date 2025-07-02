using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerInventory.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedcountrytocityinInventoryentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Inventories");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Inventories",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Inventories");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Inventories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
