using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        #region get

        [HttpGet]
        public async Task<ActionResult<List<Afiliado>>> Get()
        {
            return await dbContext.Afiliados.ToListAsync();
        }

        [HttpGet("{Cuil}")]
        public async Task<ActionResult<Afiliado>> Get(string Cuil)
        {
            var afiliado = await dbContext.Afiliados.Where(a => a.Cuil == Cuil).FirstOrDefaultAsync();

            if (afiliado == null)
            {
                return NotFound($"El afiliado con cuil {Cuil} no existe. Verifique los numeros");
            }
            else
            {
                return afiliado;
            }
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
