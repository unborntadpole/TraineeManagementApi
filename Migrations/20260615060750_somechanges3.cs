using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class somechanges3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 6, 15, 6, 7, 49, 955, DateTimeKind.Utc).AddTicks(2694), new DateTime(2026, 6, 15, 6, 7, 49, 955, DateTimeKind.Utc).AddTicks(2943) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 6, 15, 6, 6, 40, 337, DateTimeKind.Utc).AddTicks(7497), new DateTime(2026, 6, 15, 6, 6, 40, 337, DateTimeKind.Utc).AddTicks(7674) });
        }
    }
}
