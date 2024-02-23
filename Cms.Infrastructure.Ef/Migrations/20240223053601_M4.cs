using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "OrderItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "Order",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "File",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "CommodityCategoryLink",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "CommodityCategory",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "Commodity",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "CartItem",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SystemName",
                table: "Cart",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "File");

            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "CommodityCategoryLink");

            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "CommodityCategory");

            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "Commodity");

            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "SystemName",
                table: "Cart");
        }
    }
}
