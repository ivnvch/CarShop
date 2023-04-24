using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWorkShop.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProfileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileId",
                table: "Owners");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfileId",
                table: "Owners",
                type: "int",
                nullable: true);
        }
    }
}
