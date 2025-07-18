using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationViajeCero.Migrations
{
    /// <inheritdoc />
    public partial class AddUnavailableField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Unavailable",
                table: "Requests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unavailable",
                table: "Requests");
        }
    }
}
