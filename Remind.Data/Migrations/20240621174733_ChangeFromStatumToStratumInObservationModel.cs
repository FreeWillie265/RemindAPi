using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Remind.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFromStatumToStratumInObservationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Statum",
                table: "Observations",
                newName: "Stratum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stratum",
                table: "Observations",
                newName: "Statum");
        }
    }
}
