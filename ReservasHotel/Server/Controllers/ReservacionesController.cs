
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    [ApiController]
    [Route("api/Fechas")]
    public class ReservacionesController :ControllerBase
    {
        private readonly Context context;

        #region Ctor
        public ReservacionesController(Context context)
        {
            this.context = context;
        }
        #endregion


        #region Gets

        

        [HttpGet]
        public async Task<ActionResult<List<Reservaciones>>> Get()
        {

            return await context.Reservaciones.ToListAsync(); 
        }

        #endregion


        [HttpPost] // prueba, se va a agregar desde el controlador rva
        public async Task<ActionResult<Reservaciones>> Post(Reservaciones reservaciones)
        {
            try
            {
                context.Reservaciones.Add(reservaciones);
                await context.SaveChangesAsync();

                return reservaciones;

            }
            catch (Exception)
            {

                return BadRequest("No se completo la carga del nuevo usuario");
            }
        }

    }
}
