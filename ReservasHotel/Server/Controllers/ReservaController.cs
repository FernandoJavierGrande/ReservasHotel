using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/Reserva")]
    public class ReservaController : ControllerBase
    {
        private readonly Context dbcontext;


        #region ctor
        public ReservaController(Context dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        #endregion

        [HttpGet]
        public async Task<ActionResult<List<Reserva>>> Get()
          => await dbcontext.Reservas.ToListAsync();


        [HttpGet("{resid}")]
        public async Task<ActionResult<List<Reserva>>> Getid(int resid)
        {
            var reservas = await dbcontext.Reservas
                .Where(r => r.Id == resid)
                .Include(x => x.Reservaciones)
                .ToListAsync();  

            return reservas;
        }

        [HttpPost("/AgregarNuevaReserva")] // recibe una reserva y la hab creando las reservaciones correspondientes y guarda
        public async Task<ActionResult<Reserva>> Post(Reserva reserva)
        {
            reserva.UsuarioId = int.Parse(User.Claims.Where(x => x.Type == "Id").Select(i => i.Value).First());

            if ( reserva.F_inicio > reserva.F_fin)
                return BadRequest("Inconsistencias en las fechas.");

            Reservacion reservacion;

            int canDeDias = (reserva.F_fin - reserva.F_inicio).Days + 1;
            int cantDeHab = reserva.HabitacionesEnLaReserva.Count();

            Console.WriteLine($" dias: {canDeDias}, cant hab {cantDeHab} ");

            try
            {
                for (int j = 0; j < cantDeHab; j++)
                {
                    Console.WriteLine($" j = {j} valor de hab[j] = {reserva.HabitacionesEnLaReserva[j]} ");

                    for (int i = 0; i < canDeDias; i++)
                    {
                        reservacion = new Reservacion();

                        reservacion.HabitacionId = reserva.HabitacionesEnLaReserva[j];

                        reservacion.Fecha = reserva.F_inicio.AddDays(i);

                        Console.WriteLine($"fecha {reserva.F_inicio.AddDays(i)} , res.fech {reservacion.Fecha}");

                        reservacion.Cant_Huespedes = reserva.PaxPorHabitacion[j];

                        reserva.Reservaciones.Add(reservacion);

                        Console.WriteLine($" i= {i} ");
                    }
                }
                dbcontext.Reservas.Add(reserva);

                await dbcontext.SaveChangesAsync();

                return Ok(reserva);
            }
            catch (Exception e)
            {
                return BadRequest("No pudo agendar la reserva, vuelva a intentarlo " + e);
            }

        }

        

        [HttpDelete]
        public async Task<ActionResult> EliminarRva(Reserva reserva)
        {

            try
            {
                var ExisteReserva = await dbcontext.Reservas.AnyAsync(r => r.Id == reserva.Id);
                //var reservaciones = await dbcontext.Reservaciones.AnyAsync( x => x.ReservaId == reserva.Id);
                

                if (ExisteReserva)
                {
                    dbcontext.Reservas.Remove(reserva);

                    await dbcontext.SaveChangesAsync();
                    return Ok("Se elimino exitosamente");
                }
                else
                {
                    return NotFound("La reserva no existe");
                }
                
            }
            catch (Exception)
            {

                return BadRequest("no se pudo eliminar");
            }

        }

    }
}
