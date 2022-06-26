using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAppWithSignalR.Api.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvartarSourceName",
                table: "TblUsers",
                newName: "AvatarSourceName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AvatarSourceName",
                table: "TblUsers",
                newName: "AvartarSourceName");
        }
    }
}
