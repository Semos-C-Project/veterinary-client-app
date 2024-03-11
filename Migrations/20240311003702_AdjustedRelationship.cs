using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veterinary_client_app.Migrations
{
    /// <inheritdoc />
    public partial class AdjustedRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Owners_OwnerId1",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnerId1",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Pets");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "OwnerId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Owners_OwnerId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets");

            migrationBuilder.AddColumn<long>(
                name: "OwnerId1",
                table: "Pets",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId1",
                table: "Pets",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Owners_OwnerId1",
                table: "Pets",
                column: "OwnerId1",
                principalTable: "Owners",
                principalColumn: "OwnerId");
        }
    }
}
