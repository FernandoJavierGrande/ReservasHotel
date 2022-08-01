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

        



        [HttpPost]
        public async Task<ActionResult<Reserva>> PostRva(Reserva reserva)
        {
            try
            {
                dbcontext.Reservas.Add(reserva);
                await dbcontext.SaveChangesAsync();

                return reserva;
            }
            catch (Exception)
            {
                return BadRequest("No pudo agendar la reserva, vuelva a intentarlo ");
                
            }
        }

    }
}
