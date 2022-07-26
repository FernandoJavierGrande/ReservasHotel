using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class user_reserva : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Usuarios_UsuarioId",
                table: "Reservas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Usuarios_UsuarioId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Reservas");
        }
    }
}
