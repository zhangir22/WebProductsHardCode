using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HardCodeWebApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryAndUpdateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AdditionalValues",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Products",
                newName: "AdditionalValues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalValues",
                table: "Products",
                newName: "Color");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Weight",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalValues",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
