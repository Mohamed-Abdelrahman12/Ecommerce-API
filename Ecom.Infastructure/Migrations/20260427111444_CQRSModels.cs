using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CQRSModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProdutId",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WishLists");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "ProdutId",
                table: "WishLists",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_ProdutId",
                table: "WishLists",
                newName: "IX_WishLists_ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "WishLists",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_WishLists_UserEmail_ProductId",
                table: "WishLists",
                columns: new[] { "UserEmail", "ProductId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Products_ProductId",
                table: "WishLists");

            migrationBuilder.DropIndex(
                name: "IX_WishLists_UserEmail_ProductId",
                table: "WishLists");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "WishLists",
                newName: "ProdutId");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_ProductId",
                table: "WishLists",
                newName: "IX_WishLists_ProdutId");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "WishLists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WishLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "WishLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WishLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "WishLists",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Products_ProdutId",
                table: "WishLists",
                column: "ProdutId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
