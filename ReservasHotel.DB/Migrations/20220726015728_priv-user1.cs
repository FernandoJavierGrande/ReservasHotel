using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class privuser1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PrivilegioId",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PrivilegioId",
                table: "Usuarios",
                column: "PrivilegioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Privilegios_PrivilegioId",
                table: "Usuarios",
                column: "PrivilegioId",
                principalTable: "Privilegios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Privilegios_PrivilegioId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Privilegios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_PrivilegioId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<string>(
                name: "PrivilegioId",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
