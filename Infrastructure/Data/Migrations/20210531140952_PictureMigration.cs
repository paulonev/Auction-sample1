using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class PictureMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "Pictures",
                newName: "PictureUri");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pictures",
                type: "TEXT",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "PictureUri",
                table: "Pictures",
                newName: "Url");
        }
    }
}
