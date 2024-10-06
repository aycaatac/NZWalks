using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.API.Migrations
{
    /// <inheritdoc />
    public partial class addName2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AddedName",
                table: "AspNetUsers",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "AspNetUsers",
                newName: "AddedName");
        }
    }
}
