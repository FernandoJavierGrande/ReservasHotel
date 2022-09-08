using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ReservasHotel.DB.Data;
using ReservasHotel.DB.Data.Entidades;
using System.Security.Claims;

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


        [HttpPost("/login/{usuario},{clave}")]
        public async Task<ActionResult> Get(string usuario, string clave)
        {
            try
            {
                var User = context.Usuarios      
                    .Where(x => x.NombreUsuario == usuario && x.pass == clave)
                    .FirstOrDefault();

                if (User == null)
                    throw new Exception("");


                var claims = new List<Claim> //guarda los datos de la sesion 
                {
                    new Claim(ClaimTypes.Name, usuario),
                    new Claim("Id", User.Id.ToString()),
                    new Claim("Leg", User.Legajo.ToString()),
                };

                

                foreach (var item in claims)    //prueba
                    Console.WriteLine($"+++++++{item.Type} = {item.Value}");


                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return Ok($"id user {User}");
            }
            catch (Exception e)
            {
                return BadRequest("El usuario o contraseña no son correctos " + e );
            }
        }
    }
}
