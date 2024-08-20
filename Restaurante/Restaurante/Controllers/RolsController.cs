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
    public class RolsController : Controller
    {
        private readonly RestauranteContext _context;

        public RolsController(RestauranteContext context)
        {
            _context = context;
        }

        // GET: Rols
        public async Task<IActionResult> Index()
        {
            /*var roles = await _context.Roles.ToListAsync();
            return View(roles);*/

            return View(await _context.Roles.ToListAsync());
        }

        // GET: Almacenes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var almacen = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (almacen == null)
            {
                return NotFound();
            }

            return View(almacen);
        }

        // GET: Rols/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Almacenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_almacen,Nombre,Ubicacion")] Rol almacen)
        {
            if (ModelState.IsValid)
            {
                // Verificar si ya existe un almacén con el mismo nombre
                if (_context.Roles.Any(a => a.Nombre == almacen.Nombre))
                {
                    ModelState.AddModelError("Nombre", "Ya existe un almacén con este nombre.");
                    return View(almacen);
                }

                _context.Add(almacen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(almacen);
        }

        // GET: Almacenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var almacen = await _context.Roles.FindAsync(id);
            if (almacen == null)
            {
                return NotFound();
            }

            return View(almacen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_almacen,Nombre,Ubicacion")] Rol almacen)
        {
            if (id != almacen.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Verificar si ya existe otro almacén con el mismo nombre
                if (_context.Roles.Any(a => a.Id != id && a.Nombre == almacen.Nombre))
                {
                    ModelState.AddModelError("Nombre", "Ya existe un almacén con este nombre.");
                    return View(almacen);
                }

                try
                {
                    _context.Update(almacen);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlmacenExists(almacen.Id))
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

            return View(almacen);
        }

        // GET: Almacenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Roles == null)
            {
                return NotFound();
            }

            var almacen = await _context.Roles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (almacen == null)
            {
                return NotFound();
            }

            return View(almacen);
        }

        // POST: Almacenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Roles == null)
            {
                return Problem("Entity set 'SinmpleContext.Almacen'  is null.");
            }
            var almacen = await _context.Roles.FindAsync(id);
            if (almacen != null)
            {
                _context.Roles.Remove(almacen);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlmacenExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }





        // POST: Rols/Create
      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] Rol rol)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: Rols/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: Rols/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Rol rol)
        {
            if (id != rol.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolExists(rol.Id))
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
            return View(rol);
        }

        // GET: Rols/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // POST: Rols/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol != null)
            {
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool RolExists(int id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
        // GET: Rol
        /* public async Task<IActionResult> Index()
         {
             return View(await _context.Roles.ToListAsync());
         }*/
        /* public async Task<IActionResult> Index()
         {
             var roles = await _context.Roles.ToListAsync();
             return View(roles);
         }


         // GET: Rol/Create
         public IActionResult Create()
         {
             return View();
         }

         // POST: Rol/Create
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("Nombre")] Rol rol)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(rol);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(rol);
         }

         // GET: Rol/Edit/5
         public async Task<IActionResult> Edit(int id)
         {
             var rol = await _context.Roles.FindAsync(id);
             if (rol == null)
             {
                 return NotFound();
             }
             return View(rol);
         }

         // POST: Rol/Edit/5
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Rol rol)
         {
             if (id != rol.Id)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(rol);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!RolExists(rol.Id))
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
             return View(rol);
         }

         // GET: Rol/Delete/5
         public async Task<IActionResult> Delete(int id)
         {
             var rol = await _context.Roles
                 .FirstOrDefaultAsync(m => m.Id == id);
             if (rol == null)
             {
                 return NotFound();
             }

             return View(rol);
         }

         // POST: Rol/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
         {
             var rol = await _context.Roles.FindAsync(id);
             if (rol != null)
             {
                 _context.Roles.Remove(rol);
                 await _context.SaveChangesAsync();
             }
             return RedirectToAction(nameof(Index));
         }

         private bool RolExists(int id)
         {
             return _context.Roles.Any(e => e.Id == id);
         }*/
        
    }
}



