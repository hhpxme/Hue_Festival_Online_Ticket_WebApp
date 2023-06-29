using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HF_WEB_API.Migrations
{
    public partial class EditNewsesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Newses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Newses",
                type: "nvarchar(450)",
                nullable: true);
        }
    }
}
