using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class changeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Records_RecordId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RecordId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "RecordId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RecordId",
                table: "Cars",
                column: "RecordId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Records_RecordId",
                table: "Cars",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Records_RecordId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_RecordId",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "RecordId",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_RecordId",
                table: "Cars",
                column: "RecordId",
                unique: true,
                filter: "[RecordId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Records_RecordId",
                table: "Cars",
                column: "RecordId",
                principalTable: "Records",
                principalColumn: "Id");
        }
    }
}
