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
        {
            return await dbcontext.Reservas.ToListAsync();
        }

        [HttpGet("{resid}")]
        public async Task<ActionResult<List<Reserva>>> Getid(int resid)
        {
            var reservaciones = await dbcontext.Reservas
                .Where(r => r.Id == resid)
                .Include(x => x.Reservaciones)
                .ToListAsync();

            return reservaciones;
        }




        [HttpPost]
        public async Task<ActionResult<Reserva>> PostRva(Reserva reserva, int idHab,string check, string obs, int pax = 0)
        {
            try
            {
                dbcontext.Reservas.Add(reserva).ToString();
                await dbcontext.SaveChangesAsync();

                int cantidad = (reserva.F_fin - reserva.F_inicio).Days;
                Reservaciones diaReservado = new Reservaciones();
                ReservacionesController saving = new ReservacionesController(dbcontext);

                for (int i = 0; i < cantidad; i++)
                {
                    diaReservado.HabitacionId = idHab;
                    diaReservado.Fecha = reserva.F_inicio.AddDays(i);
                    diaReservado.ReservaId = reserva.Id;
                    diaReservado.Cant_Huespedes = pax;
                    diaReservado.CheckInOut = check;
                    diaReservado.Obs = obs;
                    diaReservado.FechaCreacion = DateTime.Now;

                    Console.WriteLine($"iteracion {i}  {diaReservado.Fecha}");


                    await saving.GuardarDia(diaReservado);
                    await dbcontext.SaveChangesAsync();
                }
     

                return reserva;
            }
            catch (Exception)
            {
                return BadRequest("No pudo agendar la reserva, vuelva a intentarlo ");
                
            }
        }

    }
}
