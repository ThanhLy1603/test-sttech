using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProject.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Pending_Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ST_Book_ST_Category_CategoryId1",
                table: "ST_Book");

            migrationBuilder.DropIndex(
                name: "IX_ST_Book_CategoryId1",
                table: "ST_Book");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "ST_Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId1",
                table: "ST_Book",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ST_Book_CategoryId1",
                table: "ST_Book",
                column: "CategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_Book_ST_Category_CategoryId1",
                table: "ST_Book",
                column: "CategoryId1",
                principalTable: "ST_Category",
                principalColumn: "Id");
        }
    }
}
