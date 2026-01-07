using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DSHOP.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Cartfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_Products_ProductId",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_cars_Users_UserId",
                table: "cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cars",
                table: "cars");

            migrationBuilder.RenameTable(
                name: "cars",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_cars_UserId",
                table: "Carts",
                newName: "IX_Carts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Products_ProductId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "cars");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_UserId",
                table: "cars",
                newName: "IX_cars_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cars",
                table: "cars",
                columns: new[] { "ProductId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_cars_Products_ProductId",
                table: "cars",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cars_Users_UserId",
                table: "cars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
