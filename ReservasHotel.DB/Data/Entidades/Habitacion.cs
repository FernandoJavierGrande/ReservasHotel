using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data.Entidades
{
    [Index(nameof(NHab), Name = "nHab_Uq", IsUnique = true)]
    public class Habitacion
    {
        [Required]
        [Key]
        [RegularExpression("(^[0-9]+$)", ErrorMessage = "Solo se permiten números")]
        public string NHab { get; set; }
        [Required]
        public string Tipo { get; set; }
        public string Obs { get; set; }

        #region lista   
        public List<Reservacion> Reservaciones { get; set; }
        #endregion
    }
}
