using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShippingAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_OrderId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_OrderId",
                table: "ShippingAddresses",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_OrderId",
                table: "ShippingAddresses");

            migrationBuilder.DropIndex(
                name: "IX_ShippingAddresses_UserId",
                table: "ShippingAddresses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingAddresses",
                table: "ShippingAddresses",
                columns: new[] { "UserId", "OrderId" });
        }
    }
}
