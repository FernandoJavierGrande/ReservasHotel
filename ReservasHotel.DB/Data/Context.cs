using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data
{
    public class Context : DbContext
    {
        #region Tablas

        
        public DbSet<Usuario> Usuarios { get; set; } // crea una tabla con las prop de usuario de nombre usuarios

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Reservacion> Reservaciones { get; set; }

        public DbSet<Habitacion> Habitaciones { get; set; }

        public DbSet<EstadoPago> EstadosDePago { get; set; }

        public DbSet<Privilegio> Privilegios { get; set; }

        public DbSet<Afiliado> Afiliados { get; set; }

        #endregion
        
        
        protected override void OnModelCreating(ModelBuilder builder) // pk compuesto
        {
            builder.Entity<Reservacion>().HasKey(table => new {
                table.HabitacionId,
                table.Fecha
            });
        }
        public Context(DbContextOptions options) : base(options)
        {
        }
    }
}


//"Server=DESKTOP-55OJM29; Database=Pub; Trusted_Connection=true;"
