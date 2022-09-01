using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AvcolCoCurricularWebsite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    StaffID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(maxLength: 35, nullable: false),
                    FirstName = table.Column<string>(maxLength: 35, nullable: false),
                    TeacherCode = table.Column<string>(nullable: true),
                    HireDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.StaffID);
                });

            migrationBuilder.CreateTable(
                name: "Activity",
                columns: table => new
                {
                    ActivityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(maxLength: 35, nullable: false),
                    SignUpForm = table.Column<string>(nullable: false),
                    StaffID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activity", x => x.ActivityID);
                    table.ForeignKey(
                        name: "FK_Activity_Staff_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInformation",
                columns: table => new
                {
                    StaffID = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 10, nullable: false),
                    ECPhoneNumber = table.Column<string>(maxLength: 10, nullable: false),
                    ECRelationship = table.Column<string>(maxLength: 56, nullable: false),
                    CitizenStatus = table.Column<string>(nullable: false),
                    Ethnicity = table.Column<string>(maxLength: 56, nullable: false),
                    EmailAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInformation", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_PersonalInformation_Staff_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staff",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    ClubID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<string>(maxLength: 3, nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Club", x => x.ClubID);
                    table.ForeignKey(
                        name: "FK_Club_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Music",
                columns: table => new
                {
                    MusicID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<string>(maxLength: 3, nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Music", x => x.MusicID);
                    table.ForeignKey(
                        name: "FK_Music_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PerformingArt",
                columns: table => new
                {
                    PerformingArtID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<string>(maxLength: 3, nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformingArt", x => x.PerformingArtID);
                    table.ForeignKey(
                        name: "FK_PerformingArt_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScholarshipTutorial",
                columns: table => new
                {
                    ScholarshipTutorialID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<string>(maxLength: 3, nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScholarshipTutorial", x => x.ScholarshipTutorialID);
                    table.ForeignKey(
                        name: "FK_ScholarshipTutorial_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sport",
                columns: table => new
                {
                    SportID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<string>(maxLength: 3, nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sport", x => x.SportID);
                    table.ForeignKey(
                        name: "FK_Sport_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTutorial",
                columns: table => new
                {
                    SubjectTutorialID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<int>(nullable: false),
                    RoomNumber = table.Column<string>(maxLength: 3, nullable: false),
                    Day = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTutorial", x => x.SubjectTutorialID);
                    table.ForeignKey(
                        name: "FK_SubjectTutorial_Activity_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activity",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activity_StaffID",
                table: "Activity",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Club_ActivityID",
                table: "Club",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Music_ActivityID",
                table: "Music",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_PerformingArt_ActivityID",
                table: "PerformingArt",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_ScholarshipTutorial_ActivityID",
                table: "ScholarshipTutorial",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_Sport_ActivityID",
                table: "Sport",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTutorial_ActivityID",
                table: "SubjectTutorial",
                column: "ActivityID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "Music");

            migrationBuilder.DropTable(
                name: "PerformingArt");

            migrationBuilder.DropTable(
                name: "PersonalInformation");

            migrationBuilder.DropTable(
                name: "ScholarshipTutorial");

            migrationBuilder.DropTable(
                name: "Sport");

            migrationBuilder.DropTable(
                name: "SubjectTutorial");

            migrationBuilder.DropTable(
                name: "Activity");

            migrationBuilder.DropTable(
                name: "Staff");
        }
    }
}
