using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class SecMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "trans",
                nullable: false,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "trans",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
