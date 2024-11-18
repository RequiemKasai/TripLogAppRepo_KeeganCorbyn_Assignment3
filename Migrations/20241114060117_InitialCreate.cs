using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripLogApp_KeeganCorbyn_Assignment3.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    TripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Accommodation = table.Column<string>(type: "nvarchar(max)", nullable: true), // Nullable
                    AccommodationPhone = table.Column<string>(type: "nvarchar(max)", nullable: true), // Nullable
                    AccommodationEmail = table.Column<string>(type: "nvarchar(max)", nullable: true), // Nullable
                    ThingToDo1 = table.Column<string>(type: "nvarchar(max)", nullable: true), // Nullable
                    ThingToDo2 = table.Column<string>(type: "nvarchar(max)", nullable: true), // Nullable
                    ThingToDo3 = table.Column<string>(type: "nvarchar(max)", nullable: true)  // Nullable
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.TripId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
