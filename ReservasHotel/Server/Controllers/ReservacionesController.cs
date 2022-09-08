
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
        private readonly Context dbcontext;

        #region Ctor
        public ReservacionesController(Context context)
        {
            this.dbcontext = context;
        }
        #endregion


        #region Gets

        

        [HttpGet]
        public async Task<ActionResult<List<Reservacion>>> Get()
        {

            return await dbcontext.Reservaciones.ToListAsync(); 
        }

        [HttpGet("/DiasReservados")]
        public async Task<ActionResult<List<Reservacion>>> GetReservas(DateTime fecha, int cantidad = 10)
        {
            DateTime fechaLimite = fecha.AddDays(cantidad);

            var diasReservados = dbcontext.Reservaciones.Where(
                d => d.Fecha >= fecha && d.Fecha <= fechaLimite)
                .ToListAsync();

            return await diasReservados;
        }


        #endregion


        #region post

        [HttpPost("/agregarReservaciones")]
        public async Task<ActionResult<List<Reservacion>>> GuardarDia(List<Reservacion> AgregarReservaciones)
        {

            foreach (var item in AgregarReservaciones) //valida que no exista una uq dentro de la reserva
            {
                var ocupado = dbcontext.Reservaciones.Where(x => x == item);
                if (ocupado.Contains(item))
                {
                    var salida = dbcontext.Habitaciones.Where(h => h.Id == item.HabitacionId).Select(x => x.N_DeHabitacion).FirstOrDefault();

                    return BadRequest($"La habitacion N° {salida} en la fecha fecha {item.Fecha} no esta disponible");
                }
            }

            try
            {
                foreach (var item in AgregarReservaciones)
                {
                    dbcontext.Reservaciones.Add(item);
                }
                var resp = await dbcontext.SaveChangesAsync();

                return AgregarReservaciones.ToList();
            }
            catch (Exception)
            {
                return BadRequest("No se completo el guardado ");

            }

        }
        #endregion

        [HttpDelete]
        public async Task<ActionResult> ElimDia(Reserva reserva, DateTime dia, int NumHab)
        {
            try
            {
                var idHab = await dbcontext.Habitaciones.Where( h => h.N_DeHabitacion == NumHab).Select( x => x.Id).FirstOrDefaultAsync();

                if (idHab == 0)
                {
                    return BadRequest("La habitacion no es correcta");
                }

                var reservacion = await dbcontext.Reservaciones
                    .Where(d => d.Fecha == dia && d.HabitacionId == idHab)
                    .FirstOrDefaultAsync();

                if (reservacion != null)
                {
                    dbcontext.Reservaciones.Remove(reservacion);
                    await dbcontext.SaveChangesAsync();

                    return Ok("Se elimino correctamente");
                }
                else
                {
                    return NotFound("no existe una reservacion en esta fecha y habitacion");
                }
            }
            catch (Exception)
            {

                return BadRequest("No se pudo eliminar");
            }
        } 
    }
}
