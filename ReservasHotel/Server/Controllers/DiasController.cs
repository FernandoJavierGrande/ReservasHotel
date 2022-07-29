using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    [ApiController]
    [Route("api/fechas")]
    public class DiasController : ControllerBase
    {
        
        private readonly Context dbcontext;

        #region ctor

        public DiasController( Context dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        #endregion




        [HttpPost]
        public async Task<ActionResult<Dia>> Post(Dia dia)
        {
            
            var ocupado = dbcontext.DiasReservas.Where(x => x == dia);

            if (!ocupado.Contains(dia))
            {
                
                try
                {
                    dbcontext.DiasReservas.Add(dia);

                    await dbcontext.SaveChangesAsync();

                    return dia;
                }
                catch (Exception e)
                {
                    return BadRequest($"No se puedo agendar el dia {dia.Fecha} al calendario. " + e.Message);
                }
            }
            else
            {
                return BadRequest("Esta habitacion se encuentra ocupada en esta fecha.");
            }

            
        }


    }
}
