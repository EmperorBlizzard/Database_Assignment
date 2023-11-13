using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Assignment_API.Migrations
{
    /// <inheritdoc />
    public partial class ChangednamefromTypetoInformationTypeandValuetoInformationValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "CustomerInformationTypes",
                newName: "InformationType");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "CustomerInformations",
                newName: "InformationValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InformationType",
                table: "CustomerInformationTypes",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "InformationValue",
                table: "CustomerInformations",
                newName: "Value");
        }
    }
}
