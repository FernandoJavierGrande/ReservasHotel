using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data.Entidades
{
    public class Reserva: BaseEntidad
    {
        
        [Required(ErrorMessage = "Las fechas son obligatorias")]
        public DateTime F_inicio { get; set; }
        [Required(ErrorMessage = "Las fechas son obligatorias")]
        public DateTime F_fin { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int AfiliadoId { get; set; }        
        [Required]
        public string EstadoPago { get; set; }
        [Required]
        public bool Estado { get; set; }
        public string Obs { get; set; }

        #region lista
        public List<Reservacion> Reservaciones { get; set; }


        #endregion
    }
}
