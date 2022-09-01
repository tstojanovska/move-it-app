using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoveITApp.DataAccess.Migrations
{
    public partial class seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Distance",
                table: "Proposals",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "DistanceRules",
                columns: new[] { "Id", "FixedPrice", "From", "PricePerKm", "To" },
                values: new object[,]
                {
                    { 1, 1000, 1, 10, 50 },
                    { 2, 5000, 50, 8, 100 },
                    { 3, 10000, 100, 7, null }
                });

            migrationBuilder.InsertData(
                table: "MovingObjectRules",
                columns: new[] { "Id", "FixedPrice", "MovingObjectType" },
                values: new object[] { 1, 5000, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DistanceRules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DistanceRules",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DistanceRules",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MovingObjectRules",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<long>(
                name: "Distance",
                table: "Proposals",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
