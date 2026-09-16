using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoProject.Migrations
{
    /// <inheritdoc />
    public partial class Add_Category_And_Book_Custom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ST_Book_ST_Category_CategoryId",
                table: "ST_Book");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ST_Category",
                newName: "Is_Deleted");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "ST_Category",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ST_Book",
                newName: "Is_Deleted");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "ST_Book",
                newName: "Created_at");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ST_Book",
                newName: "Category_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ST_Book_CategoryId",
                table: "ST_Book",
                newName: "IX_ST_Book_Category_Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_ST_Book_Price",
                table: "ST_Book",
                sql: "[Price] >= 0");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_Book_ST_Category_Category_Id",
                table: "ST_Book",
                column: "Category_Id",
                principalTable: "ST_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ST_Book_ST_Category_Category_Id",
                table: "ST_Book");

            migrationBuilder.DropCheckConstraint(
                name: "CK_ST_Book_Price",
                table: "ST_Book");

            migrationBuilder.RenameColumn(
                name: "Is_Deleted",
                table: "ST_Category",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "ST_Category",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "Is_Deleted",
                table: "ST_Book",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "ST_Book",
                newName: "CreationTime");

            migrationBuilder.RenameColumn(
                name: "Category_Id",
                table: "ST_Book",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ST_Book_Category_Id",
                table: "ST_Book",
                newName: "IX_ST_Book_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ST_Book_ST_Category_CategoryId",
                table: "ST_Book",
                column: "CategoryId",
                principalTable: "ST_Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
