using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class afiliado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Afiliado",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Reservas");

            migrationBuilder.AddColumn<int>(
                name: "AfiliadoId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "EstadosDePago",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Afiliados",
                columns: table => new
                {
                    Cuil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Afiliados", x => x.Cuil);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_AfiliadoId",
                table: "Reservas",
                column: "AfiliadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Afiliados_AfiliadoId",
                table: "Reservas",
                column: "AfiliadoId",
                principalTable: "Afiliados",
                principalColumn: "Cuil",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Afiliados_AfiliadoId",
                table: "Reservas");

            migrationBuilder.DropTable(
                name: "Afiliados");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_AfiliadoId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "AfiliadoId",
                table: "Reservas");

            migrationBuilder.AddColumn<string>(
                name: "Afiliado",
                table: "Reservas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "Reservas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Reservas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "EstadosDePago",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
