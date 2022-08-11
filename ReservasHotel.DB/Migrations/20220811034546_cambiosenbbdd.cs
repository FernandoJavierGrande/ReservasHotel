using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class cambiosenbbdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_EstadosDePago_EstadoPagoId",
                table: "Reservas");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Privilegios_PrivilegioId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "EstadosDePago");

            migrationBuilder.DropTable(
                name: "Privilegios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PrivilegioId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_EstadoPagoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "PrivilegioId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EstadoPagoId",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "Privilegio",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EstadoPago",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Privilegio",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EstadoPago",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "PrivilegioId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstadoPagoId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EstadosDePago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDePago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilegios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Permiso = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilegios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PrivilegioId",
                table: "Usuarios",
                column: "PrivilegioId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Privilegios_PrivilegioId",
                table: "Usuarios",
                column: "PrivilegioId",
                principalTable: "Privilegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
