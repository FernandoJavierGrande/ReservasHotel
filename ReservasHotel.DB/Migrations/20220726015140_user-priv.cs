using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasHotel.DB.Migrations
{
    public partial class userpriv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Privilegio",
                table: "Usuarios",
                newName: "PrivilegioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrivilegioId",
                table: "Usuarios",
                newName: "Privilegio");
        }
    }
}
