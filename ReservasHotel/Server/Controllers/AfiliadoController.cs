using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    //[Authorize]
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
            var afiliado = await dbContext.Afiliados.Where(a => a.Cuil == Cuil).Include(x => x.Rva).FirstOrDefaultAsync();

            if (afiliado == null)
            {
                
                return BadRequest($"El afiliado con cuil {Cuil} no existe. Verifique los numeros");
            }
            else
            {
                return afiliado;
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Afiliado>> GetRva(int id)
        {
            var afiliado = await dbContext.Afiliados.Where(r => r.Id == id).FirstOrDefaultAsync(); 

            if (afiliado != null)
            {
                return afiliado;
            }
            return NotFound("no se encontro");
        }
        //[HttpGet("Reservas")]


        //public async Task<ActionResult<List<Afiliado>>> GetCuil(string cuil)
        //{
        //    var reserva = await dbContext.Afiliados.Where(r => r.Cuil == cuil)
        //        .Include(a => a.Rva)
        //        .ToListAsync();

        //    if (reserva != null)
        //    {
        //        return reserva;
        //    }
        //    return NotFound("no se encontro");
        //}

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

        [HttpDelete("{cuil}")]
        public ActionResult Delete(string cuil)
        {
            var afEliminar = dbContext.Afiliados.Where(x => x.Cuil == cuil).FirstOrDefault();

            if (afEliminar == null)
            {
                return NotFound($"El registro {cuil} no fue encontrado");
            }

            try
            {
                dbContext.Afiliados.Remove(afEliminar);
                dbContext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("no se pudo eliminar");
            }
        }
    }

}
