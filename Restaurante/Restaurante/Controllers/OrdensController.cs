using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    public class OrdensController : Controller
    {
        private readonly RestauranteContext _context;

        public OrdensController(RestauranteContext context)
        {
            _context = context;
        }

        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            var restauranteContext = _context.Ordenes.Include(o => o.Usuario);
            return View(await restauranteContext.ToListAsync());
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordenes
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordens/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Ordens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId")] Orden orden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", orden.UsuarioId);
            return View(orden);
        }

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordenes.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", orden.UsuarioId);
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", orden.UsuarioId);
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ordenes == null)
            {
                return NotFound();
            }

            var orden = await _context.Ordenes
                .Include(o => o.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ordenes == null)
            {
                return Problem("Entity set 'RestauranteContext.Ordenes'  is null.");
            }
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden != null)
            {
                _context.Ordenes.Remove(orden);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
          return (_context.Ordenes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
