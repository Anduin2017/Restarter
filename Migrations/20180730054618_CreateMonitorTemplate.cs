using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Restarter.Migrations
{
    public partial class CreateMonitorTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MonitorTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MonitorName = table.Column<string>(nullable: true),
                    RequestPath = table.Column<string>(nullable: true),
                    IsPostMethod = table.Column<bool>(nullable: false),
                    Form = table.Column<string>(nullable: true),
                    ExpectedStatusCode = table.Column<int>(nullable: false),
                    ExpectedContent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitorTemplates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonitorTemplates");
        }
    }
}
