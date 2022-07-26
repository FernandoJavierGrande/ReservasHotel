using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EstadosDePago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosDePago", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N_DeHabitacion = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    F_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Afiliado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Pago = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreUsuario = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    pass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Legajo = table.Column<int>(type: "int", nullable: false),
                    Privilegio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiasReservas",
                columns: table => new
                {
                    HabitacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    Cant_Huespedes = table.Column<int>(type: "int", nullable: false),
                    Late = table.Column<bool>(type: "bit", nullable: false),
                    Early = table.Column<bool>(type: "bit", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiasReservas", x => new { x.HabitacionId, x.Fecha });
                    table.ForeignKey(
                        name: "FK_DiasReservas_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiasReservas_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "diaIdHab_Uq",
                table: "DiasReservas",
                columns: new[] { "HabitacionId", "Fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiasReservas_ReservaId",
                table: "DiasReservas",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "NUsuario_Uq",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiasReservas");

            migrationBuilder.DropTable(
                name: "EstadosDePago");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "Reservas");
        }
    }
}
