using Microsoft.EntityFrameworkCore.Migrations;

namespace Restarter.Migrations
{
    public partial class LinkServerToHealthMontor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonitorId",
                table: "Servers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servers_MonitorId",
                table: "Servers",
                column: "MonitorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servers_MonitorTemplates_MonitorId",
                table: "Servers",
                column: "MonitorId",
                principalTable: "MonitorTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servers_MonitorTemplates_MonitorId",
                table: "Servers");

            migrationBuilder.DropIndex(
                name: "IX_Servers_MonitorId",
                table: "Servers");

            migrationBuilder.DropColumn(
                name: "MonitorId",
                table: "Servers");
        }
    }
}
