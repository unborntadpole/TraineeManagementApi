using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TraineeManagementApi.Migrations
{
    /// <inheritdoc />
    public partial class LearningTaskItem4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "PasswordHash", "Role", "UpdatedDate", "Username" },
                values: new object[] { 1L, new DateTime(2026, 6, 15, 6, 5, 26, 176, DateTimeKind.Utc).AddTicks(3158), "samriddh.singh@zeuslearning.com", "AQAAAAIAAYagAAAAEP+QfNdJZtmZSCQUsvRTWt8NlKADYbY44q8GjYNIUhVn8c2ANxKiw50h4muvwf7ydg==", "Admin", new DateTime(2026, 6, 15, 6, 5, 26, 176, DateTimeKind.Utc).AddTicks(3309), "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
