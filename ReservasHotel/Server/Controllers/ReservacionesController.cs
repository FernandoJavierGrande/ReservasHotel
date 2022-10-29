
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    [ApiController]
    [Route("api/Fechas")]
    public class ReservacionesController : ControllerBase
    {
        private readonly Context dbcontext;

        #region Ctor
        public ReservacionesController(Context context)
        {
            this.dbcontext = context;
        }
        #endregion


        #region Gets



        [HttpGet("{fe}")]
        public async Task<ActionResult<List<List<Reservacion>>>> Get(string fe, int dias = 7)
            //necesito que retorne una lista de dias ocupados y no
        {
             
            
            DateTime fecha = DateTime.Parse(fe);

            var habitaciones = await dbcontext.Habitaciones.ToListAsync();

            List<Reservacion> reservaciones = new List<Reservacion>();
            List<List<Reservacion>> listaDeListas = new List<List<Reservacion>>();

            foreach (var habitacion in habitaciones)
            {
                reservaciones = new List<Reservacion>();
                
                for (int i = 0; i < dias; i++) 
                {
                    bool disp = await dbcontext.Reservaciones
                        .AnyAsync(r => r.Fecha == fecha.AddDays(i) && r.HabitacionId == habitacion.NHab);

                    if (disp)
                    {
                        Reservacion res;
                        res = await dbcontext.Reservaciones
                                    .Where(r => r.HabitacionId == habitacion.NHab && r.Fecha == fecha.AddDays(i))
                                    .FirstAsync();
                        
                        reservaciones.Add(res);
                    }
                    else
                    {
                        reservaciones.Add(new Reservacion());
                        reservaciones[i].Fecha = fecha.AddDays(i);
                        reservaciones[i].HabitacionId = habitacion.NHab;
                    }  
                }
                listaDeListas.Add(reservaciones);
            }
            return listaDeListas;
        }

        //[HttpGet("/DiasReservados")]
        //public async Task<ActionResult<List<Reservacion>>> GetReservas(DateTime fecha, int cantidad = 10)
        //{
        //    DateTime fechaLimite = fecha.AddDays(cantidad);

        //    var diasReservados = dbcontext.Reservaciones.Where(
        //        d => d.Fecha >= fecha && d.Fecha <= fechaLimite)
        //        .ToListAsync();

        //    return await diasReservados;
        //}


        #endregion


        #region post

        //[HttpPost("/agregarReservaciones")]
        public async Task<ActionResult<List<Reservacion>>> GuardarDia(List<Reservacion> AgregarReservaciones)
        {

            foreach (var item in AgregarReservaciones) //valida que no exista una uq dentro de la reserva
            {
                var ocupado = dbcontext.Reservaciones.Where(x => x == item);
                if (ocupado.Contains(item))
                {
                    var salida = dbcontext.Habitaciones.Where(h => h.NHab
                    == item.HabitacionId).Select(x => x.NHab).FirstOrDefault();

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


        #region Del
        
        [HttpDelete]   // elimina una reservacion
        public async Task<ActionResult> ElimDia(Reserva reserva, DateTime dia, string NumHab)
        {
            try
            {
                var idHab = await dbcontext.Habitaciones
                    .Where( h => h.NHab == NumHab).Select( x => x.NHab).FirstOrDefaultAsync();

                if (idHab == null)
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

        #endregion


        #region Update
        /* recibe una reserva, la reservacion a eliminar[0] y la reservacion a agregar[1] (en un mismo dato) modifica la fecha de la reservacion si está disponible 
           cambia la fecha de la reserva de ing o de salida +1 */
        [HttpPut]
        public async Task<ActionResult> Update(Reserva reserva) // si se tiene que cambiar el registro entero
        {

            Console.WriteLine($"Reservacion[0] elim = {reserva.Reservaciones[0]} reservacion[2] add = {reserva.Reservaciones[1]}");

            if (reserva.Reservaciones[0] == null || reserva.Reservaciones[1] == null)
                return BadRequest("Error intente nuevamente");


            // busca si esta disponible la hab en la fecha determinada(uq) 
            var disponible = await dbcontext.Reservaciones.AnyAsync(r => r == reserva.Reservaciones[1]); 

            if (disponible)
                return BadRequest("La fecha no esta disponible");

            reserva = ModFechasReserva(reserva);

            try
            {
                dbcontext.Reservaciones.Add(reserva.Reservaciones[1]);

                dbcontext.Reservaciones.Remove(reserva.Reservaciones[0]); // se elimina la "modificada"

                dbcontext.Reservas.Update(reserva);

                await dbcontext.SaveChangesAsync();

                return Ok("Se guardo correctamente");

            }
            catch (Exception e)
            {

                return BadRequest("error " + e);
            }
        }


        //[HttpPut("ModificarHuespedes")] // cambiar para modificar solo uno?
        //public async Task<ActionResult> Put(Reservacion reservacion)
        //{
        //    var res = await dbcontext.Reservaciones
        //        .Where(r => r.HabitacionId == reservacion.HabitacionId && r.ReservaId == reservacion.ReservaId)
        //        .ToListAsync();

        //    try
        //    {
        //        foreach (var item in res)
        //        {
        //            item.Cant_Huespedes = reservacion.Cant_Huespedes;

        //            dbcontext.Reservaciones.Update(item);
        //        }

        //        await dbcontext.SaveChangesAsync();
        //        return Ok();
        //    }
        //    catch (Exception)
        //    {

        //        return BadRequest("No se actualizo correctamente");
        //    }
        //}

        #endregion


        #region Metodos 
        private Reserva ModFechasReserva( Reserva reserva)
        {
            if (reserva.F_inicio > reserva.Reservaciones[1].Fecha ) // si la fecha nueva es menor a la f inicio orig se pone en inicio
            {
                if (reserva.Reservaciones[0].Fecha == reserva.F_fin) // en este caso se estaria "corriendo" para atras una fecha 
                {
                    reserva.F_fin = reserva.F_fin.AddDays(-1);
                }
                reserva.F_inicio = reserva.Reservaciones[1].Fecha;
            }
            else if (reserva.F_fin < reserva.Reservaciones[1].Fecha)// si la fecha nueva es mayor al final de la orig se extiende 
            {
                if (reserva.Reservaciones[0].Fecha == reserva.F_inicio) // aca se "adelanta" un dia eliminando el primero
                {
                    reserva.F_inicio = reserva.F_inicio.AddDays(1);
                }
                reserva.F_fin = reserva.Reservaciones[1].Fecha;
            }

            return reserva;
        }


        #endregion

    }
}
