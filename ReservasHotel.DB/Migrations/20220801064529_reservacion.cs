using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class reservacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInOut",
                table: "Reservaciones");

            migrationBuilder.DropColumn(
                name: "FechaCreacion",
                table: "Reservaciones");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Reservaciones");

            migrationBuilder.DropColumn(
                name: "Obs",
                table: "Reservaciones");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheckInOut",
                table: "Reservaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCreacion",
                table: "Reservaciones",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Reservaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Obs",
                table: "Reservaciones",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
