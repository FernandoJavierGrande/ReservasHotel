using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    //[Authorize]
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

        #region get
        [HttpGet]
        public async Task<ActionResult<List<Habitacion>>> Get()
        {
            return await dbcontext.Habitaciones.ToListAsync();
        }


        [HttpGet("{NumHabitacion}")]
        public async Task<ActionResult<Habitacion>> Get(string NumHabitacion)
        {
            var habitacion = await dbcontext.Habitaciones.Where(
                e => e.NHab == NumHabitacion).FirstOrDefaultAsync();
            if (habitacion==null)
            {
                return NotFound($"La habitacion {NumHabitacion} no existe");
            }
            else
            {
                return habitacion;
            }
        }

        //[HttpGet("TipoDeHabitacion")]    //devuelve los tipos de hab con sus numeros
        //public ActionResult<List<string>> GetCode(string tipo)
        //{
        //    var numeros = dbcontext.Habitaciones.Where(x => x.Tipo == tipo).Select(a => a.N_DeHabitacion).ToList();

        //    if (numeros.Count == 0)
        //    {
        //        return NotFound($"La habitacion {tipo} no fue encontrada");
        //    }

        //    return numeros;

        //}
        #endregion

        #region post
        [HttpPost]
        public async Task<ActionResult<Habitacion>> Post(Habitacion Habitacion)
        {
            try
            {
                dbcontext.Habitaciones.Add(Habitacion);

                await dbcontext.SaveChangesAsync();
                return Habitacion;
            }
            catch (Exception e)
            {
                return BadRequest("No se completo el agregado de una nueva habitacion " + e);
            }
        }
        #endregion

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var habElim = dbcontext.Habitaciones.Where(x => x.NHab == id).FirstOrDefault();

            if (habElim == null)
            {
                return NotFound($"El registro {id} no fue encontrado");
            }

            try
            {
                dbcontext.Habitaciones.Remove(habElim);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("no se pudo eliminar");
            }
        }
        [HttpPut("{Nhab}")]
        public ActionResult Put(string Nhab, [FromBody] Habitacion habitacion)
        {
            if (Nhab != habitacion.NHab)
            {
                return BadRequest("Datos incorrectos");
            }

            var resp = dbcontext.Habitaciones.Where(e => e.NHab == Nhab).FirstOrDefault();

            if (resp == null)
            {
                return NotFound($"La hab {Nhab} no existe.");
            }

            resp.NHab = habitacion.NHab;
            resp.Tipo = habitacion.Tipo;
            resp.Obs = habitacion.Obs;

            try
            {
                dbcontext.Habitaciones.Update(resp);
                dbcontext.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest($"No se actualizo {e}");
            }
        }
    }
}
