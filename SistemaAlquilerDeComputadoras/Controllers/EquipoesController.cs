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
    public class EquipoesController : Controller
    {
        private readonly MyContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public EquipoesController(MyContext context, IWebHostEnvironment _webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = _webHostEnvironment;
        }

        // GET: Equipoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipos.ToListAsync());
        }

        // GET: Equipoes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // GET: Equipoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Codigo,Almacenamiento,Estado,UrlFoto,Pantalla,Procesador,Ram,Resolucion")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipo);
        }

        // GET: Equipoes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return View(equipo);
        }

        // POST: Equipoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Codigo,Almacenamiento,Estado,FotoFile,Pantalla,Procesador,Ram,Resolucion")] Equipo equipo)
        {
            if (id != equipo.Codigo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (equipo.FotoFile != null)
                    {
                        await GuardarImagen(equipo);
                    }
                  
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoExists(equipo.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipo);
        }

        private async Task GuardarImagen(Equipo equipo)
        {
            //formar el nombre de la foto
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string extension = Path.GetExtension(equipo.FotoFile!.FileName);
            string nameFoto = $"{equipo.Codigo}{equipo.Procesador}{extension}";

            equipo.UrlFoto = nameFoto;

            //copiar la foto en el proyecto
            string path = Path.Combine($"{wwwRootPath}/fotos/", nameFoto);
            var fileStream = new FileStream(path, FileMode.Create);
            await equipo.FotoFile.CopyToAsync(fileStream);
        }

        // GET: Equipoes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Codigo == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        // POST: Equipoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(string id)
        {
            return _context.Equipos.Any(e => e.Codigo == id);
        }
    }
}
