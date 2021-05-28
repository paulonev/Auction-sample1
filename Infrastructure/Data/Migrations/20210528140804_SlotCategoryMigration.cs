using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class SlotCategoryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Categories_CategoryId",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_CategoryId",
                table: "Slots");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_CategoryId",
                table: "Slots",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Categories_CategoryId",
                table: "Slots",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Categories_CategoryId",
                table: "Slots");

            migrationBuilder.DropIndex(
                name: "IX_Slots_CategoryId",
                table: "Slots");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_CategoryId",
                table: "Slots",
                column: "CategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Categories_CategoryId",
                table: "Slots",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
