using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data.Entidades
{
    public class Habitacion: BaseEntidad
    {
       
        [Required]
        public int N_DeHabitacion { get; set; }
        [Required]
        public string Tipo { get; set; }
        public string Obs { get; set; }

        #region lista   
        public List<Dia> Dias{ get; set; }
        #endregion
    }
}
