using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.App.Migrations
{
    public partial class addedNewASSETtABLE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CAssets_CourseAssetsId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseAssetsId",
                table: "Courses",
                newName: "CAssetsId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseAssetsId",
                table: "Courses",
                newName: "IX_Courses_CAssetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CAssets_CAssetsId",
                table: "Courses",
                column: "CAssetsId",
                principalTable: "CAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CAssets_CAssetsId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CAssetsId",
                table: "Courses",
                newName: "CourseAssetsId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CAssetsId",
                table: "Courses",
                newName: "IX_Courses_CourseAssetsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CAssets_CourseAssetsId",
                table: "Courses",
                column: "CourseAssetsId",
                principalTable: "CAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
