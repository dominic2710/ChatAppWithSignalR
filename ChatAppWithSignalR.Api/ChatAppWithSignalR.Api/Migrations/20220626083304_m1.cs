using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatAppWithSignalR.Api.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoreSalt",
                table: "TblUsers",
                newName: "StoredSalt");

            migrationBuilder.RenameColumn(
                name: "IsOline",
                table: "TblUsers",
                newName: "IsOnline");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StoredSalt",
                table: "TblUsers",
                newName: "StoreSalt");

            migrationBuilder.RenameColumn(
                name: "IsOnline",
                table: "TblUsers",
                newName: "IsOline");
        }
    }
}
