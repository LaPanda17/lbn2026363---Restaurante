using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Models;
using System.Security.Claims;



namespace Restaurante.Controllers
{
    public class AccesosController : Controller
    {
        //Conexion con el contexto
        private readonly RestauranteContext _context;

        public AccesosController(RestauranteContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        int intentos = 0;
        // en aqui se realiza la confirmacion de los datos
        [HttpPost]

        public async Task<IActionResult> Index(Usuario usuario)
        {
            //string contra = CypherHelper.CifradoHash(usuario.Contraseña);
            var usuarioConf = _context.Usuarios.Where(u => u.Nombre == usuario.Nombre && u.PasswordHash == usuario.PasswordHash).FirstOrDefault();

            if (usuarioConf != null)
            {
                // int id = usuarioConf.id_usuario;
                // Simulate a user with claims (not recommended for production)
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier,usuarioConf.Id.ToString()),
                        new Claim(ClaimTypes.Name , usuarioConf.Nombre),


                    };
                var rol = _context.Roles.Where(u => u.Id == usuarioConf.Id).FirstOrDefault();
                if (rol != null)
                {
                    claims.Add(new Claim(ClaimTypes.Role, rol.Nombre));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    /*
                    var user = _context.Usuario.Include(l => l.TipoPersona).Where(u => u.IdUsuario == usuarioConf.IdUsuario);
                    //HttpContext.Session.SetInt32("IdUsuario", id);
                    TempData["IdUsuario"] = id;
                    DateTime now = DateTime.Now;
                    TempData["FechaInicio"] = now;

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,usuarioConf.NomUsuario),
                        new Claim("IdUsuario",id.ToString()),

                    };
                    foreach (var tipoPer in user)
                    {
                        var per = tipoPer.TipoPersona.NombreTipo.ToString();
                        claims.Add(new Claim(ClaimTypes.Role, per));
                    }

                //HttpContext.Session["idDelUsuario"] =id;
                if (tipo.NombreTipo == "Administrador")
                    {
                        return RedirectToAction(nameof(Admin));
                    }
                    else
                    {

                        return RedirectToAction(nameof(Comun));
                    }



                if (tipoCli != null)
                {
                    id = tipoCli.id_cliente;
                    tipo = "Cliente";
                    claims.Add(
                       new Claim(ClaimTypes.Role, tipo)
                   );
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    */
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }



                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Privacy", "Home");
            }
            //return RedirectToAction("Privacy", "Home");
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
