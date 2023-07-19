using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduHome.App.Migrations
{
    public partial class createdTeacherHobbyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TeacherHobbies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "TeacherHobbies");
        }
    }
}
