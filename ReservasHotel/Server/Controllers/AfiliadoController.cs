using Microsoft.AspNetCore.Mvc;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    [ApiController]
    [Route("api/Afiliados")]
    public class AfiliadoController : ControllerBase
    {
        private readonly Context dbContext;

        #region const
        public AfiliadoController(Context dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion


        [HttpPost]
        public async Task<ActionResult<Afiliado>> Post(Afiliado afiliado)
        {
            try
            {
                dbContext.Afiliados.Add(afiliado);

                await dbContext.SaveChangesAsync();

                return afiliado;
            }
            catch (Exception e)
            {
                return BadRequest("No se completo la operacion de agregado de afiliado " + e.Message);
                
            }
        }
    }
}
