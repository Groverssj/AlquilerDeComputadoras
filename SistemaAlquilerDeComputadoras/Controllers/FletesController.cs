﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaAlquilerDeComputadoras.Data;
using SistemaAlquilerDeComputadoras.Models;

namespace SistemaAlquilerDeComputadoras.Controllers
{
    public class FletesController : Controller
    {
        private readonly SistemaAlquilerDeComputadorasContext _context;

        public FletesController(SistemaAlquilerDeComputadorasContext context)
        {
            _context = context;
        }

        // GET: Fletes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Flete.ToListAsync());
        }

        // GET: Fletes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flete = await _context.Flete
                .FirstOrDefaultAsync(m => m.NroRecibo == id);
            if (flete == null)
            {
                return NotFound();
            }

            return View(flete);
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

            var flete = await _context.Flete.FindAsync(id);
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

            var flete = await _context.Flete
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
            var flete = await _context.Flete.FindAsync(id);
            if (flete != null)
            {
                _context.Flete.Remove(flete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FleteExists(int id)
        {
            return _context.Flete.Any(e => e.NroRecibo == id);
        }
    }
}
