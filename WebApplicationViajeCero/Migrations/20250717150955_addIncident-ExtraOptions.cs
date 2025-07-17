using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationViajeCero.Migrations
{
    /// <inheritdoc />
    public partial class addIncidentExtraOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraOptions",
                table: "Requests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Incident",
                table: "Requests",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraOptions",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "Incident",
                table: "Requests");
        }
    }
}
