using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAlquilerDeComputadoras.Contexto;

using SistemaAlquilerDeComputadoras.Models;

namespace SistemaAlquilerDeComputadoras.Controllers
{
    public class FletesController : Controller
    {
        private readonly MyContext _context;

        public FletesController(MyContext context)
        {
            _context = context;
        }

        // GET: Fletes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fletes.ToListAsync());
        }


        // GET: Fletes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fletes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NroRecibo,Costo,Fecha,HoraInicio,HoraFinal")] Flete flete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flete);
        }

        // GET: Fletes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flete = await _context.Fletes.FindAsync(id);
            if (flete == null)
            {
                return NotFound();
            }
            return View(flete);
        }

        // POST: Fletes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NroRecibo,Costo,Fecha,HoraInicio,HoraFinal")] Flete flete)
        {
            if (id != flete.NroRecibo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FleteExists(flete.NroRecibo))
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
            return View(flete);
        }

        // GET: Fletes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flete = await _context.Fletes
                .FirstOrDefaultAsync(m => m.NroRecibo == id);
            if (flete == null)
            {
                return NotFound();
            }

            return View(flete);
        }

        // POST: Fletes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flete = await _context.Fletes.FindAsync(id);
            if (flete != null)
            {
                _context.Fletes.Remove(flete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleteExists(int id)
        {
            return _context.Fletes.Any(e => e.NroRecibo == id);
        }

        public async Task<IActionResult> IniciarTiempo(int id)
        {
            var flete = await _context.Fletes.FindAsync(id);
            if (flete == null)
            {
                return NotFound();
            }

            flete.HoraInicio = DateTime.Now;
            _context.Update(flete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DetenerTiempo(int id)
        {
            var flete = await _context.Fletes.FindAsync(id);
            if (flete == null)
            {
                return NotFound();
            }

            flete.HoraFinal = DateTime.Now;
            TimeSpan duration = flete.HoraFinal - flete.HoraInicio;
            flete.Costo = (decimal)duration.TotalHours * 2; 
            _context.Update(flete);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Fletes/GenerarRecibo/5
        public async Task<IActionResult> GenerarRecibo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flete = await _context.Fletes.FindAsync(id);
            if (flete == null)
            {
                return NotFound();
            }

            var recibo = new Recibo
            {
                Flete = flete,
                Cliente = new Cliente()
            };

            return View("recibo", recibo);
        }

        // POST: Fletes/GenerarRecibo/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerarRecibo(int id, [Bind("Flete,Cliente")] Recibo recibo)
        {
            if (id != recibo.Flete.NroRecibo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var clienteExistente = await _context.Clientes.FindAsync(recibo.Cliente.Ci);
                if (clienteExistente == null)
                {
                    _context.Clientes.Add(recibo.Cliente);
                    await _context.SaveChangesAsync();
                }



                return RedirectToAction(nameof(Index));
            }
            return View("recibo", recibo);
        }
        



    }
}
