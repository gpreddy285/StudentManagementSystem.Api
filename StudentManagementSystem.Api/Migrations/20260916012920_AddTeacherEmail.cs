using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTeacherEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TecaherEmail",
                table: "Teachers",
                newName: "TeacherEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeacherEmail",
                table: "Teachers",
                newName: "TecaherEmail");
        }
    }
}
