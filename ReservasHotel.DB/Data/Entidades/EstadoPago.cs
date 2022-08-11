using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data.Entidades
{
    public class EstadoPago:BaseEntidad
    {
        [Required]
        public string Estado { get; set; }

        #region lista
        //public List<Reserva> Reservas { get; set; } // un pago esta en 0 o muchas reservas
        #endregion
    }
}
