using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationViajeCero.Migrations
{
    /// <inheritdoc />
    public partial class NotnullUnavailableFine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unavailable",
                table: "Requests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Incident",
                table: "Requests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ExtraOptions",
                table: "Requests",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Unavailable",
                keyValue: null,
                column: "Unavailable",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Unavailable",
                table: "Requests",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "Incident",
                keyValue: null,
                column: "Incident",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Incident",
                table: "Requests",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Requests",
                keyColumn: "ExtraOptions",
                keyValue: null,
                column: "ExtraOptions",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "ExtraOptions",
                table: "Requests",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
