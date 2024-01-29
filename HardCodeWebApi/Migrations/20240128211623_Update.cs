using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HardCodeWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Products__Catego__5070F446",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK__Products__ProptI__5165187F",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProptId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProptId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Propts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Propts");

            migrationBuilder.AddColumn<int>(
                name: "ProptId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProptId",
                table: "Products",
                column: "ProptId");

            migrationBuilder.AddForeignKey(
                name: "FK__Products__Catego__5070F446",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__Products__ProptI__5165187F",
                table: "Products",
                column: "ProptId",
                principalTable: "Propts",
                principalColumn: "Id");
        }
    }
}
