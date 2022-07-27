using Microsoft.AspNetCore.Mvc;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    [ApiController]
    [Route("api/Habitacion")]
    public class HabitacionController: ControllerBase
    {
        private readonly Context dbcontext;
        #region ctor
        public HabitacionController(Context dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        #endregion

        [HttpPost]
        public async Task<ActionResult<Habitacion>> Post(Habitacion Habitacion)
        {
            try
            {
                dbcontext.Habitaciones.Add(Habitacion);

                await dbcontext.SaveChangesAsync();
                return Habitacion;
            }
            catch (Exception e )
            {
                return BadRequest("No se completo el agregado de una nueva habitacion " + e.Message);
            }
        }

    }
}
