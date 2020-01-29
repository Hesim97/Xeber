using Microsoft.EntityFrameworkCore.Migrations;

namespace Xeber.Migrations
{
    public partial class step2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ViewCount",
                table: "News",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ViewCount",
                table: "News",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
