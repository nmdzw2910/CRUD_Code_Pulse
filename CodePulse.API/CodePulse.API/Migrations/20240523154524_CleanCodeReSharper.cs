using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulse.API.Migrations
{
    /// <inheritdoc />
    public partial class CleanCodeReSharper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInformations_ShippingInformationId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingInformations",
                table: "ShippingInformations");

            migrationBuilder.RenameTable(
                name: "ShippingInformations",
                newName: "ShippingInformation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingInformation",
                table: "ShippingInformation",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInformation_ShippingInformationId",
                table: "Orders",
                column: "ShippingInformationId",
                principalTable: "ShippingInformation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInformation_ShippingInformationId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingInformation",
                table: "ShippingInformation");

            migrationBuilder.RenameTable(
                name: "ShippingInformation",
                newName: "ShippingInformations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingInformations",
                table: "ShippingInformations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInformations_ShippingInformationId",
                table: "Orders",
                column: "ShippingInformationId",
                principalTable: "ShippingInformations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
