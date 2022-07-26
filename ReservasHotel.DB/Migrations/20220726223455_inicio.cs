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
                name: "Afiliados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cuil = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afiliados", x => x.Id);
                });

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
                name: "Habitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    N_DeHabitacion = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Privilegios",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Permiso = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Privilegios", x => x.Id);
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
                    PrivilegioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Privilegios_PrivilegioId",
                        column: x => x.PrivilegioId,
                        principalTable: "Privilegios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    F_inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    F_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AfiliadoId = table.Column<int>(type: "int", nullable: false),
                    EstadoPagoId = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false),
                    Obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_Afiliados_AfiliadoId",
                        column: x => x.AfiliadoId,
                        principalTable: "Afiliados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_EstadosDePago_EstadoPagoId",
                        column: x => x.EstadoPagoId,
                        principalTable: "EstadosDePago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiasReservas",
                columns: table => new
                {
                    HabitacionId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservaId = table.Column<int>(type: "int", nullable: false),
                    Cant_Huespedes = table.Column<int>(type: "int", nullable: true),
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
                name: "IX_Reservas_AfiliadoId",
                table: "Reservas",
                column: "AfiliadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_EstadoPagoId",
                table: "Reservas",
                column: "EstadoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioId",
                table: "Reservas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PrivilegioId",
                table: "Usuarios",
                column: "PrivilegioId");

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
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Afiliados");

            migrationBuilder.DropTable(
                name: "EstadosDePago");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Privilegios");
        }
    }
}
