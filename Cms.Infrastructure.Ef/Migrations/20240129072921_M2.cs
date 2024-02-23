using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cms.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FileId",
                table: "OrderItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_FileId",
                table: "OrderItem",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_File_FileId",
                table: "OrderItem",
                column: "FileId",
                principalTable: "File",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_File_FileId",
                table: "OrderItem");

            migrationBuilder.DropIndex(
                name: "IX_OrderItem_FileId",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "OrderItem");
        }
    }
}
