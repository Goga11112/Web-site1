using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_site1.Migrations
{
    /// <inheritdoc />
    public partial class int_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldMaxLength: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 6);
        }
    }
}
