using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoreliMobilyaDeneme.Migrations
{
    /// <inheritdoc />
    public partial class addImagePath7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath7",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath7",
                table: "Products");
        }
    }
}
