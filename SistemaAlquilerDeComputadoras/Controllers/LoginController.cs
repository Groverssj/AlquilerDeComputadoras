using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SistemaAlquilerDeComputadoras.Contexto;
using SistemaAlquilerDeComputadoras.Models;
using System.Security.Claims;

namespace SistemaAlquilerDeComputadoras.Controllers
{
    public class LoginController : Controller
    {
        MyContext _context;

        public LoginController(MyContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = _context.Usuarios
                .Where(x => x.Cuenta == email)
                .Where(x => x.Password == password)
                .FirstOrDefault();
            if (usuario != null)
            {
                await SetAuthenticationInCookie(usuario);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["LoginError"] = "Cuenta o Password incorrecte";
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Login");
        }
        //public async task<iactionresult> logout()
        //{
        //    return redirecttoaction("index", "login");
        //}
        private async Task SetAuthenticationInCookie(Usuario? user)
        {
            var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.NombreCompleto!),
                    new Claim("Cuenta", user.Cuenta!),
                    new Claim("Id", user.Ci.ToString()),
                    new Claim(ClaimTypes.Role, user.Rol!.ToString()),
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}
