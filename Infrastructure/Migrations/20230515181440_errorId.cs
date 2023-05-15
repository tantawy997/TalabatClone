using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class errorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_delivaryMethods_delivaryMethodid",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "delivaryMethodid",
                table: "orders",
                newName: "delivaryMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_delivaryMethodid",
                table: "orders",
                newName: "IX_orders_delivaryMethodId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "delivaryMethods",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_delivaryMethods_delivaryMethodId",
                table: "orders",
                column: "delivaryMethodId",
                principalTable: "delivaryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_delivaryMethods_delivaryMethodId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "delivaryMethodId",
                table: "orders",
                newName: "delivaryMethodid");

            migrationBuilder.RenameIndex(
                name: "IX_orders_delivaryMethodId",
                table: "orders",
                newName: "IX_orders_delivaryMethodid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "delivaryMethods",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_delivaryMethods_delivaryMethodid",
                table: "orders",
                column: "delivaryMethodid",
                principalTable: "delivaryMethods",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
