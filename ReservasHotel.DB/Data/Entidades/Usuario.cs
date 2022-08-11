using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data.Entidades
{
    [Index(nameof(NombreUsuario), Name = "NUsuario_Uq", IsUnique =true)]
    public class Usuario: BaseEntidad
    {
        #region propiedades
        [Required]
        [MaxLength(150, ErrorMessage ="El Nombre de usuario no debe superar [1] caracteres")]
        public string NombreUsuario { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "La contaseña no cumple con los requisitos minimos")]
        public string pass { get; set; }
        [Required]
        public int Legajo { get; set; }
        [Required]
        public string Privilegio { get; set; }

        #endregion

        #region listas

        public List<Reserva> Reservas { get; set; }  // un usuario hace cero o muchas reservas



        #endregion
    }
}
