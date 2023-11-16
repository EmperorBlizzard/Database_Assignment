using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database_Assignment_API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCustomerInformationandCustomerInformationtypeentitys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInformations");

            migrationBuilder.DropTable(
                name: "CustomerInformationTypes");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Customers");

            migrationBuilder.CreateTable(
                name: "CustomerInformationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InformationType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInformationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInformations",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    InformationValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInformations", x => new { x.CustomerId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_CustomerInformations_CustomerInformationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CustomerInformationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerInformations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInformations_TypeId",
                table: "CustomerInformations",
                column: "TypeId");
        }
    }
}
