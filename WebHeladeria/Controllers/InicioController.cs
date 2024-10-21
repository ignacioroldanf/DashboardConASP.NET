using Microsoft.AspNetCore.Mvc;
using WebHeladeria.Models;
using WebHeladeria.Recursos;
using WebHeladeria.Servicios.Contrato;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebHeladeria.Controllers
{
    public class InicioController : Controller
    {
        private readonly IUsuarioService _usuarioServicio;

        public InicioController(IUsuarioService usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        [HttpGet]
        public IActionResult Registrarse()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }






        [HttpPost]
        public async Task <IActionResult> Registrarse(Usuario modelo)
        {
            modelo.ContraUsuario = Utilidades.EncriptarClave(modelo.ContraUsuario);

            Usuario usuario_creado = await _usuarioServicio.SaveUsuario(modelo);

            if(usuario_creado.IdUsuario>0)
            {
                return RedirectToAction("IniciarSesion", "Inicio");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario";

            return View();
        }


        [HttpPost]
        public async Task <IActionResult> IniciarSesion(string nombre, string contra)
        {
            Usuario usuario_encontrado = await _usuarioServicio.GetUsuario(nombre,Utilidades.EncriptarClave(contra));

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "No se encontró el usuario";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario_encontrado.NombreUsuario)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };


            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, 
                new ClaimsPrincipal(claimsIdentity), properties);


            return RedirectToAction("Index", "Localidades");
        }
    }
}
