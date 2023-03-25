using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CourseShare.Repository.Migrations
{
    public partial class dbmodelsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CourseName = table.Column<string>(nullable: false),
                    CourseDescription = table.Column<string>(nullable: false),
                    CourseLink = table.Column<string>(nullable: false),
                    CourseLanguage = table.Column<string>(nullable: false),
                    CourseRating = table.Column<double>(nullable: false),
                    CourseQuiz = table.Column<string>(nullable: false),
                    AddedByUser = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MyCourses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CoursesInMyCourses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false),
                    MyCoursesId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesInMyCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoursesInMyCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesInMyCourses_MyCourses_MyCoursesId",
                        column: x => x.MyCoursesId,
                        principalTable: "MyCourses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesInMyCourses_CourseId",
                table: "CoursesInMyCourses",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesInMyCourses_MyCoursesId",
                table: "CoursesInMyCourses",
                column: "MyCoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_MyCourses_OwnerId",
                table: "MyCourses",
                column: "OwnerId",
                unique: true,
                filter: "[OwnerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesInMyCourses");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "MyCourses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
