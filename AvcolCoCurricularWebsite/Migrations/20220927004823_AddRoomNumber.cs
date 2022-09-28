namespace AvcolCoCurricularWebsite.Migrations;

public partial class AddRoomNumber : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RoomNumber",
            table: "SubjectTutorial");

        migrationBuilder.DropColumn(
            name: "RoomNumber",
            table: "Sport");

        migrationBuilder.DropColumn(
            name: "RoomNumber",
            table: "ScholarshipTutorial");

        migrationBuilder.DropColumn(
            name: "RoomNumber",
            table: "PerformingArt");

        migrationBuilder.DropColumn(
            name: "RoomNumber",
            table: "Music");

        migrationBuilder.DropColumn(
            name: "RoomNumber",
            table: "Club");

        migrationBuilder.AddColumn<string>(
            name: "RoomNumber",
            table: "Activity",
            maxLength: 3,
            nullable: false,
            defaultValue: "");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RoomNumber",
            table: "Activity");

        migrationBuilder.AddColumn<string>(
            name: "RoomNumber",
            table: "SubjectTutorial",
            type: "nvarchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "RoomNumber",
            table: "Sport",
            type: "nvarchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "RoomNumber",
            table: "ScholarshipTutorial",
            type: "nvarchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "RoomNumber",
            table: "PerformingArt",
            type: "nvarchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "RoomNumber",
            table: "Music",
            type: "nvarchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "RoomNumber",
            table: "Club",
            type: "nvarchar(3)",
            maxLength: 3,
            nullable: false,
            defaultValue: "");
    }
}