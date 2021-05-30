using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class SetAuctionIdNullMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Auctions_AuctionId",
                table: "Slots");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Auctions_AuctionId",
                table: "Slots",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slots_Auctions_AuctionId",
                table: "Slots");

            migrationBuilder.AddForeignKey(
                name: "FK_Slots_Auctions_AuctionId",
                table: "Slots",
                column: "AuctionId",
                principalTable: "Auctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
