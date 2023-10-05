using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PizzasBussinessLogicChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PizzaSize",
                table: "pizzas");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "pizzas");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "pizzas",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "pizzas");

            migrationBuilder.AddColumn<int>(
                name: "PizzaSize",
                table: "pizzas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "pizzas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
