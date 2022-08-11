using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
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



        [HttpPost("NuevaReserva")]
        public async Task<ActionResult<Reserva>> Post(Reserva NuevaReserva)
        {
            try
            {
                dbcontext.Reservas.Add(NuevaReserva);
                 await dbcontext.SaveChangesAsync();

                return NuevaReserva;
            }
            catch (Exception)
            {

               return BadRequest("no se guardó la reserva ");
            }
        }


        [HttpPost("/Reservaciones")]
        public async Task<ActionResult<Reserva>> Post(Reserva reserva, int idHab, int pax = 10)
        {
            if (reserva.F_inicio > reserva.F_fin)
            {
                return BadRequest("Inconsistencias en las fechas.");
            }

            Reservacion reservacion = new Reservacion();

            int cantidad = (reserva.F_fin - reserva.F_inicio).Days + 1;
            try
            {
                for (int i = 0; i < cantidad; i++)
                {
                    reservacion.HabitacionId = idHab;
                    reservacion.Fecha = reserva.F_inicio.AddDays(i);
                    reservacion.Cant_Huespedes = pax;


                    reserva.Reservaciones.Add(reservacion);
                }
                dbcontext.Reservas.Add(reserva);
                await dbcontext.SaveChangesAsync();

                return Ok(reserva);
            }
            catch (Exception)
            {

                return BadRequest("No pudo agendar la reserva, vuelva a intentarlo ");
            }

        }
        [HttpPost]
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
