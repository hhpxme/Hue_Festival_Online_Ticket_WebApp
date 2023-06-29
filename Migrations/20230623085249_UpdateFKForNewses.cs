using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HF_WEB_API.Migrations
{
    public partial class UpdateFKForNewses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Newses",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Newses");
        }
    }
}
