using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuctionService.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Colour",
                table: "Items",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "ReservedPrice",
                table: "Auctions",
                newName: "ReservePrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Items",
                newName: "Colour");

            migrationBuilder.RenameColumn(
                name: "ReservePrice",
                table: "Auctions",
                newName: "ReservedPrice");
        }
    }
}
