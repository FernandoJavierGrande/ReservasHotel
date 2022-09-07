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

        [HttpPost("/agregarHabitaciones")]
        public async Task<ActionResult<List<Reservacion>>> GuardarDia(List<Reservacion> AgregarHabitaciones)
        {
           
            foreach (var item in AgregarHabitaciones) //valida que no exista una uq dentro de la reserva
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
                foreach (var item in AgregarHabitaciones)
                {
                    dbcontext.Reservaciones.Add(item);
                    
                }
                var resp = await dbcontext.SaveChangesAsync();
                
                return AgregarHabitaciones.ToList();
            }
            catch (Exception)
            {
                return BadRequest("No se completo el guardado " );
                
            }

        }
    }
}
