using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAlquilerDeComputadoras.Contexto;
using SistemaAlquilerDeComputadoras.Models;

namespace SistemaAlquilerDeComputadoras.Controllers
{
    public class CatalogoController : Controller
    {
        private readonly MyContext _context;

        public CatalogoController(MyContext context)
        {
            _context = context;
        }

        // GET: Equipoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipos.ToListAsync());
        }

        public async Task<IActionResult> Login()
        {
            return RedirectToAction("Index", "Login");
        }
        public async Task<IActionResult> Logout()
        {
            return RedirectToAction("Index", "Catalogo");
        }
    }
}
