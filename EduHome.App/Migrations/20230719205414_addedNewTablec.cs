using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.App.Migrations
{
    public partial class addedNewTablec : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CAssets_courseAssetsId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseAssestsId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "courseAssetsId",
                table: "Courses",
                newName: "CourseAssetsId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_courseAssetsId",
                table: "Courses",
                newName: "IX_Courses_CourseAssetsId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseAssetsId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CAssets_CourseAssetsId",
                table: "Courses",
                column: "CourseAssetsId",
                principalTable: "CAssets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CAssets_CourseAssetsId",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "CourseAssetsId",
                table: "Courses",
                newName: "courseAssetsId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_CourseAssetsId",
                table: "Courses",
                newName: "IX_Courses_courseAssetsId");

            migrationBuilder.AlterColumn<int>(
                name: "courseAssetsId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseAssestsId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CAssets_courseAssetsId",
                table: "Courses",
                column: "courseAssetsId",
                principalTable: "CAssets",
                principalColumn: "Id");
        }
    }
}
