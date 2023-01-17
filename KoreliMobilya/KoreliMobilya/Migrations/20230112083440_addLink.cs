using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoreliMobilyaDeneme.Migrations
{
    /// <inheritdoc />
    public partial class addLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Banners");
        }
    }
}
