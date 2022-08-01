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
        public async Task<ActionResult<Reserva>> PostRva(Reserva reserva, int idHab, int pax = 0)
        {
            int cantidad = (reserva.F_fin.AddDays(1) - reserva.F_inicio).Days;

            bool ocupado = validarDisponibilidad(idHab,reserva.F_inicio,cantidad);

            Console.WriteLine($"vuelve de valDis {ocupado}");
            if (!ocupado)
            {
                return BadRequest( $"La habitacion se encuentra ocupada en la/s fecha/s solicitada");
            }

            try 
            {
                dbcontext.Reservas.Add(reserva).ToString();
                await dbcontext.SaveChangesAsync();

                
                Reservacion diaReservado = new Reservacion();
                ReservacionesController saving = new ReservacionesController(dbcontext);

                for (int i = 0; i < cantidad; i++)
                {
                    diaReservado.HabitacionId = idHab;
                    diaReservado.Fecha = reserva.F_inicio.AddDays(i);
                    diaReservado.ReservaId = reserva.Id;
                    diaReservado.Cant_Huespedes = pax;

                    Console.WriteLine($"iteracion {i}  {diaReservado.Fecha}");


                    await saving.GuardarDia(diaReservado);
                    
                }

                await dbcontext.SaveChangesAsync();
                return reserva;
            }
            catch (Exception)
            {
                return BadRequest("No pudo agendar la reserva, vuelva a intentarlo ");
                
            }
        }


        private bool validarDisponibilidad(int idhab,DateTime fInicio, int cantidad )
        {
            DateTime date;

            for (int i = 0; i < cantidad; i++)
            {
                date = fInicio.AddDays(i);
                
                var ocupado = dbcontext.Reservaciones.Select(r => r.Fecha == date && r.HabitacionId == idhab).FirstOrDefault();
                if (ocupado)
                {
                    Console.WriteLine($"metodo validar disponibilidad valor de ocupado: {ocupado}. ret false ");
                    return false;
                }
            }
            Console.WriteLine("metodo validar disp, ret true");
            return true;
        }

    }
}
