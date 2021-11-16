using Microsoft.EntityFrameworkCore.Migrations;

namespace charts.web.api.Migrations
{
    public partial class phase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Nation = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Discipline = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Nation = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Discipline = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Event = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "EntriesGender",
                columns: table => new
                {
                    Discipline = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Female = table.Column<int>(type: "integer", nullable: false),
                    Male = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Medals",
                columns: table => new
                {
                    Nation = table.Column<string>(type: "text", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    Gold = table.Column<int>(type: "integer", nullable: false),
                    Silver = table.Column<int>(type: "integer", nullable: false),
                    Bronze = table.Column<int>(type: "integer", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false),
                    RankByTotal = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medals", x => x.Nation);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Discipline = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Nation = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Event = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "EntriesGender");

            migrationBuilder.DropTable(
                name: "Medals");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
