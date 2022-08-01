using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        #region get
        [HttpGet]
        public async Task<ActionResult<List<Habitacion>>> Get()
        {
            return await dbcontext.Habitaciones.ToListAsync();
        }


        [HttpGet("{NumHabitacion:int}")]
        public async Task<ActionResult<string>> Get(int NumHabitacion)
        {
            var habitacion = await dbcontext.Habitaciones.Where(
                e => e.N_DeHabitacion == NumHabitacion).FirstOrDefaultAsync();
            if (habitacion==null)
            {
                return NotFound($"La habitacion {NumHabitacion} no existe");
            }
            else
            {
                return $"Habitacion {habitacion.Tipo}. Obs: {habitacion.Obs}";
            }
        }

        [HttpGet("TipoDeHabitacion")]
        public ActionResult<List<int>> GetCode(string tipo)
        {
            var numeros = dbcontext.Habitaciones.Where(x => x.Tipo == tipo).Select(a => a.N_DeHabitacion).ToList();

            if (numeros.Count == 0)
            {
                return NotFound($"La habitacion {tipo} no fue encontrada");
            }

            return numeros;

        }



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
            catch (Exception)
            {
                return BadRequest("No se completo el agregado de una nueva habitacion ");
            }
        }
        #endregion

    }
}
