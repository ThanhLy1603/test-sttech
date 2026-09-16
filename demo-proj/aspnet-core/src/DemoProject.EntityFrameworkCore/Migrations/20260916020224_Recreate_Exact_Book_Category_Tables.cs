using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProject.Migrations
{
    /// <inheritdoc />
    public partial class Recreate_Exact_Book_Category_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_at",
                table: "ST_Category");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ST_Category");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "ST_Category");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ST_Category");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ST_Category");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ST_Category");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "ST_Book");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "ST_Book");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "ST_Book");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "ST_Book");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "ST_Book");

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Deleted",
                table: "ST_Category",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Deleted",
                table: "ST_Book",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_at",
                table: "ST_Book",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "SYSDATETIME()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Is_Deleted",
                table: "ST_Category",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_at",
                table: "ST_Category",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "ST_Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "ST_Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ST_Category",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ST_Category",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "ST_Category",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Deleted",
                table: "ST_Book",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created_at",
                table: "ST_Book",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "SYSDATETIME()");

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "ST_Book",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "ST_Book",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "ST_Book",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "ST_Book",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "ST_Book",
                type: "bigint",
                nullable: true);
        }
    }
}
