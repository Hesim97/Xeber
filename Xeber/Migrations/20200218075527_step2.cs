using Microsoft.EntityFrameworkCore.Migrations;

namespace Xeber.Migrations
{
    public partial class step2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "Langs",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DisplayName",
                table: "Langs",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
