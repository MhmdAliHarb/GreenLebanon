using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GreenLebanon.Taxi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddriverstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AspNetUsers_UserId",
                table: "Trips");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "572a1d83-2815-4eec-979b-adf231a4f3a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f3df691-8765-41d3-846f-0a1d8b175539");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d82058b-b44f-4801-b104-2bb162128144");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Trips",
                newName: "DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                newName: "IX_Trips_DriverId");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Trips",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Drivers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e2c0bcc-21e3-4874-8a17-d054bf9d733a", null, "Driver", "DRIVER" },
                    { "6ff6abb2-af64-42d8-b3ef-a3012b86321d", null, "Admin", "ADMIN" },
                    { "c58f608b-a50e-4cd7-a314-4f076e4e5b8b", null, "Client", "CLIENT" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ClientId",
                table: "Trips",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_ClientId",
                table: "Trips",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AspNetUsers_ClientId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Drivers_DriverId",
                table: "Trips");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropIndex(
                name: "IX_Trips_ClientId",
                table: "Trips");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e2c0bcc-21e3-4874-8a17-d054bf9d733a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ff6abb2-af64-42d8-b3ef-a3012b86321d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c58f608b-a50e-4cd7-a314-4f076e4e5b8b");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Trips",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                newName: "IX_Trips_UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "572a1d83-2815-4eec-979b-adf231a4f3a5", null, "Admin", "ADMIN" },
                    { "5f3df691-8765-41d3-846f-0a1d8b175539", null, "Client", "CLIENT" },
                    { "9d82058b-b44f-4801-b104-2bb162128144", null, "Driver", "DRIVER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
