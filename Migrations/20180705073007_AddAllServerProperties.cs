using Microsoft.EntityFrameworkCore.Migrations;

namespace Restarter.Migrations
{
    public partial class AddAllServerProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Architect",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiskSize",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InDomainName",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InnerIP",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Memory",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OS",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutterIP",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsageA",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UsageB",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VCPU",
                table: "Servers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VMArchitect",
                table: "Servers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Architect",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "DiskSize",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "InDomainName",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "InnerIP",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Memory",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "OS",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "OutterIP",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "UsageA",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "UsageB",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "VCPU",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "VMArchitect",
                table: "Servers");
        }
    }
}
