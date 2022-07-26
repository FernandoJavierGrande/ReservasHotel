using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class correcciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Obs",
                table: "Habitaciones",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cant_Huespedes",
                table: "DiasReservas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Obs",
                table: "Habitaciones");

            migrationBuilder.AlterColumn<int>(
                name: "Cant_Huespedes",
                table: "DiasReservas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
