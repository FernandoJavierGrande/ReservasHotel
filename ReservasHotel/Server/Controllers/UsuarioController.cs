using Microsoft.AspNetCore.Mvc;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;

namespace ReservasHotel.Server.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController : ControllerBase
    {
        private readonly Context context;

        #region ctor
        public UsuarioController(Context context)
        {
            this.context = context;
        }
        #endregion


        #region post
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario user)
        {
            try
            {
                context.Usuarios.Add(user);
                
                await context.SaveChangesAsync();

                return user;
            }
            catch (Exception)
            {

                return BadRequest("No se completo la carga del nuevo usuario");
            }
            


        }

        #endregion
    }
}
