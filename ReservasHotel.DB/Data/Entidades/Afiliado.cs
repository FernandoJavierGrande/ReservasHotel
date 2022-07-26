using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data.Entidades
{
    public class Afiliado
    {
        [Key]
        [MinLength(10, ErrorMessage = "El numero no puede ser menor a [1] caracteres")]
        public int Cuil { get; set; }
        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(150)]
        public string Apellido { get; set; }

        #region lista

        public List<Reserva> Rva { get; set; }

        #endregion

    }
}
