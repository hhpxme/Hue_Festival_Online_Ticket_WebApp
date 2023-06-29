using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HF_WEB_API.Migrations
{
    public partial class UpdateTicket1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPay",
                table: "Tickets",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPay",
                table: "Tickets");
        }
    }
}
