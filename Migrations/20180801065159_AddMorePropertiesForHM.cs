using Microsoft.EntityFrameworkCore.Migrations;

namespace Restarter.Migrations
{
    public partial class AddMorePropertiesForHM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestPath",
                table: "MonitorTemplates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MonitorName",
                table: "MonitorTemplates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedContent",
                table: "MonitorTemplates",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHttpsMethod",
                table: "MonitorTemplates",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Port",
                table: "MonitorTemplates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHttpsMethod",
                table: "MonitorTemplates");

            migrationBuilder.DropColumn(
                name: "Port",
                table: "MonitorTemplates");

            migrationBuilder.AlterColumn<string>(
                name: "RequestPath",
                table: "MonitorTemplates",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "MonitorName",
                table: "MonitorTemplates",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ExpectedContent",
                table: "MonitorTemplates",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
