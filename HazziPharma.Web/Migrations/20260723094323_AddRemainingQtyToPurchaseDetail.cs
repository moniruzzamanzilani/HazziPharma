using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HazziPharma.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddRemainingQtyToPurchaseDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingQty",
                table: "PurchaseDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingQty",
                table: "PurchaseDetails");
        }
    }
}
