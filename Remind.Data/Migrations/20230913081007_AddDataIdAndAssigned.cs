using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Remind.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDataIdAndAssigned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Traversed",
                table: "Subjects",
                newName: "Assigned");

            migrationBuilder.RenameColumn(
                name: "Etc",
                table: "Subjects",
                newName: "Note");

            migrationBuilder.AddColumn<string>(
                name: "DataId",
                table: "Subjects",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataId",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Subjects",
                newName: "Etc");

            migrationBuilder.RenameColumn(
                name: "Assigned",
                table: "Subjects",
                newName: "Traversed");
        }
    }
}
