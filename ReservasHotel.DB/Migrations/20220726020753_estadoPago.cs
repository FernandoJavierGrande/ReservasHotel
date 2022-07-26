using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class estadoPago : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pago",
                table: "Reservas",
                newName: "EstadoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_EstadoPagoId",
                table: "Reservas",
                column: "EstadoPagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_EstadosDePago_EstadoPagoId",
                table: "Reservas",
                column: "EstadoPagoId",
                principalTable: "EstadosDePago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_EstadosDePago_EstadoPagoId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_EstadoPagoId",
                table: "Reservas");

            migrationBuilder.RenameColumn(
                name: "EstadoPagoId",
                table: "Reservas",
                newName: "Pago");
        }
    }
}
