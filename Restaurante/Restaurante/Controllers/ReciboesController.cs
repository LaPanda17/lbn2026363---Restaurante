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
    public class ReciboesController : Controller
    {
        private readonly RestauranteContext _context;

        public ReciboesController(RestauranteContext context)
        {
            _context = context;
        }

        // GET: Reciboes
        public async Task<IActionResult> Index()
        {
            var restauranteContext = _context.Recibos.Include(r => r.Orden);
            return View(await restauranteContext.ToListAsync());
        }

        // GET: Reciboes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Recibos == null)
            {
                return NotFound();
            }

            var recibo = await _context.Recibos
                .Include(r => r.Orden)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recibo == null)
            {
                return NotFound();
            }

            return View(recibo);
        }

        // GET: Reciboes/Create
        public IActionResult Create()
        {
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id");
            return View();
        }

        // POST: Reciboes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrdenId,Total,Cambio")] Recibo recibo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recibo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id", recibo.OrdenId);
            return View(recibo);
        }

        // GET: Reciboes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Recibos == null)
            {
                return NotFound();
            }

            var recibo = await _context.Recibos.FindAsync(id);
            if (recibo == null)
            {
                return NotFound();
            }
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id", recibo.OrdenId);
            return View(recibo);
        }

        // POST: Reciboes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrdenId,Total,Cambio")] Recibo recibo)
        {
            if (id != recibo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recibo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReciboExists(recibo.Id))
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
            ViewData["OrdenId"] = new SelectList(_context.Ordenes, "Id", "Id", recibo.OrdenId);
            return View(recibo);
        }

        // GET: Reciboes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Recibos == null)
            {
                return NotFound();
            }

            var recibo = await _context.Recibos
                .Include(r => r.Orden)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recibo == null)
            {
                return NotFound();
            }

            return View(recibo);
        }

        // POST: Reciboes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Recibos == null)
            {
                return Problem("Entity set 'RestauranteContext.Recibos'  is null.");
            }
            var recibo = await _context.Recibos.FindAsync(id);
            if (recibo != null)
            {
                _context.Recibos.Remove(recibo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReciboExists(int id)
        {
          return (_context.Recibos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
