using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_site1.Migrations
{
    /// <inheritdoc />
    public partial class SetDefaultValueForRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
        name: "Role",
        table: "AspNetUsers",
        nullable: false,
        defaultValue: "Пользователь", // Значение по умолчанию
        oldClrType: typeof(string),
        oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
      name: "Role",
      table: "AspNetUsers",
      nullable: true,
      oldClrType: typeof(string),
      oldDefaultValue: "Пользователь");
        }
    }
}
