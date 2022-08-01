using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    
    public class DiasController : ControllerBase
    {
        
        private readonly Context dbcontext;

        #region ctor

        public DiasController( Context dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        #endregion




        //[HttpPost()]
        //public async Task<ActionResult<Reservaciones>> Post(Reservaciones dia)
        //{

        //    var ocupado = dbcontext.Reservaciones.Where(x => x == dia);

        //    if (!ocupado.Contains(dia))
        //    {

        //        try
        //        {
        //            dbcontext.Reservaciones.Add(dia);

        //            await dbcontext.SaveChangesAsync();

        //            return dia;
        //        }
        //        catch (Exception e)
        //        {
        //            return BadRequest($"No se puedo agendar el dia {dia.Fecha} al calendario. " + e.Message);
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Esta habitacion se encuentra ocupada en esta fecha.");
        //    }


        //}


        //[HttpGet]
        //public async Task<ActionResult<List<Reservaciones>>> GetReservas(DateTime fecha, int cantidad = 10)
        //{
        //    DateTime fechaLimite = fecha.AddDays(cantidad);

        //    var diasReservados = dbcontext.DiasReservas.Where(
        //        d => d.Fecha >= fecha && d.Fecha <= fechaLimite)
        //        .ToListAsync();

        //    return  await diasReservados;
        //}


    }
}
