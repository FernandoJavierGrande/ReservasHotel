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
    /* esta clase/tabla representa los dias y las habitaciones que puede tener una reseva
     puede haber varios registros que pertenecen a una misma reserva ya sea porque abarca varios dias 
    ej res 1 tiene el dia 23;24;25 o tambien porque una reserva puede incluir dos habitaciones o mas */
    
    [Index(nameof(HabitacionId),nameof(Fecha), Name = "diaIdHab_Uq", IsUnique = true)]
    //la clave unica valida que una habitacion no puede reservarse si ya esta reservada en determinada fecha o viceversa
    public class Dia
    {
        
        [Required]
        public int HabitacionId { get; set; }
        [Required]
        [DataType(DataType.Date)]   /*cambiar a string*/
        //[MaxLength(10, ErrorMessage ="El Formato de la fecha debe ser DD/MM/AAAA")]
        public DateTime Fecha { get; set; }
        [Required]
        public int ReservaId { get; set; }
        public int? Cant_Huespedes { get; set; }
        public bool Late { get; set; }
        public bool Early { get; set; }
        public string Obs { get; set; }

    }
}
