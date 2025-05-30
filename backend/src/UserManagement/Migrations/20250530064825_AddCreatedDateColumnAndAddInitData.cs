using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedDateColumnAndAddInitData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "JoinDate", "Status", "Username" },
                values: new object[,]
                {
                    { new Guid("01971fe9-5030-7517-b000-a5a7b1368270"), new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), "user1@example.com", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 0, "user1" },
                    { new Guid("01971fe9-fb80-7a02-84c7-8e6f604b3f86"), new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Utc), "user2@example.com", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "user2" },
                    { new Guid("01971fea-1cb7-7d2f-b391-4107b22812a8"), new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Utc), "user3@example.com", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "user3" },
                    { new Guid("01971fea-37cc-7bcc-8071-ecad077d0a53"), new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Utc), "user4@example.com", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "user4" },
                    { new Guid("01971fea-555d-7c5e-ac91-8138324bb6a0"), new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "user5@example.com", new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, "user5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01971fe9-5030-7517-b000-a5a7b1368270"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01971fe9-fb80-7a02-84c7-8e6f604b3f86"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01971fea-1cb7-7d2f-b391-4107b22812a8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01971fea-37cc-7bcc-8071-ecad077d0a53"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01971fea-555d-7c5e-ac91-8138324bb6a0"));

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Users");
        }
    }
}
