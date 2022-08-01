
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
        public async Task<ActionResult<List<Reservacion>>> Get()
        {

            return await context.Reservaciones.ToListAsync(); 
        }


        #endregion


        [HttpPost] 
        public async Task<ActionResult<Reservacion>> GuardarDia(Reservacion reservaciones)
        {
            var ocupado = context.Reservaciones.Where(x => x == reservaciones);

            if (!ocupado.Contains(reservaciones))
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
            else
            {
                return BadRequest($"La habitacion se encuentra ocupada en la fecha " +
                    $"{reservaciones.Fecha.Day}/{reservaciones.Fecha.Month}/{reservaciones.Fecha.Year}.");
            }
            
        }

    }
}
