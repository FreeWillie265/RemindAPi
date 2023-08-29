using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Remind.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AgeGroup = table.Column<string>(type: "TEXT", nullable: false),
                    Sex = table.Column<string>(type: "TEXT", nullable: false),
                    BlockId = table.Column<string>(type: "TEXT", nullable: false),
                    BlockSize = table.Column<int>(type: "INTEGER", nullable: false),
                    ClinicName = table.Column<string>(type: "TEXT", nullable: false),
                    District = table.Column<string>(type: "TEXT", nullable: false),
                    Clerk = table.Column<string>(type: "TEXT", nullable: false),
                    Etc = table.Column<string>(type: "TEXT", nullable: false),
                    Traversed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
