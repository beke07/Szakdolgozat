using Microsoft.EntityFrameworkCore.Migrations;

namespace Szakdoga.DAL.Migrations
{
    public partial class isDoneProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isDone",
                table: "Projects",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDone",
                table: "Projects");
        }
    }
}
