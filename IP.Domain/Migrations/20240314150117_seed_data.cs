using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IP.Domain.Migrations
{
    /// <inheritdoc />
    public partial class seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "Users",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<string>(
                name: "ImageLink",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("c17d487e-646a-47e2-9ee4-b319155e326e"), null, null, "admin", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "AvatarLink", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "Gender", "ImageLink", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Title", "TwoFactorEnabled", "UserCode", "UserName" },
                values: new object[] { new Guid("d7d6ae65-8029-46c5-a006-f89d6d04fa8c"), 0, null, "41821eaa-5117-44f7-af8f-c1d0314042a6", new DateTime(1993, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "lms@hou.edu.vn", true, "Đại học", 0, null, false, "Mở Hà Nội", false, null, "lms@hou.edu.vn", null, "AQAAAAIAAYagAAAAEMZu0A3TrqPY4d8NcXyjgjC0PeOuLHe9ZccS/Qko/jj/7rRl8HY0AyfMufE1MdrpWQ==", "024 3868 2321", false, "", null, false, null, "admin" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("c17d487e-646a-47e2-9ee4-b319155e326e"), new Guid("d7d6ae65-8029-46c5-a006-f89d6d04fa8c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c17d487e-646a-47e2-9ee4-b319155e326e"), new Guid("d7d6ae65-8029-46c5-a006-f89d6d04fa8c") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c17d487e-646a-47e2-9ee4-b319155e326e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d7d6ae65-8029-46c5-a006-f89d6d04fa8c"));

            migrationBuilder.DropColumn(
                name: "ImageLink",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dob",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
