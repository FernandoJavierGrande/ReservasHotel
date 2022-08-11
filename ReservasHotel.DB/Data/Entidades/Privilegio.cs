using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasHotel.DB.Data.Entidades
{
    public class Privilegio:BaseEntidad
    {

        [Required]
        public string Permiso { get; set; }

        #region lista
        //public List<Usuario> usuarios { get; set; }
        #endregion

    }
}
