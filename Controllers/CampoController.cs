using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Formularios.Controllers
{
    public class CampoController : Controller
    {
        private readonly AppDbContext _context;

        public CampoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Campo
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Campos.Include(c => c.Tipo);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Campo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Campos == null)
            {
                return NotFound();
            }

            var campo = await _context.Campos
                .Include(c => c.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (campo == null)
            {
                return NotFound();
            }

            return View(campo);
        }

        // GET: Campo/Create
        public IActionResult Create()
        {
            ViewData["TipoId"] = new SelectList(_context.Tipos, "Id", "Nome");
            return View();
        }

        // POST: Campo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,TipoId")] Campo campo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(campo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoId"] = new SelectList(_context.Tipos, "Id", "Nome", campo.TipoId);
            return View(campo);
        }

        // GET: Campo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campos == null)
            {
                return NotFound();
            }

            var campo = await _context.Campos.FindAsync(id);
            if (campo == null)
            {
                return NotFound();
            }
            ViewData["TipoId"] = new SelectList(_context.Tipos, "Id", "Nome", campo.TipoId);
            return View(campo);
        }

        // POST: Campo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,TipoId")] Campo campo)
        {
            if (id != campo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampoExists(campo.Id))
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
            ViewData["TipoId"] = new SelectList(_context.Tipos, "Id", "Nome", campo.TipoId);
            return View(campo);
        }

        // GET: Campo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Campos == null)
            {
                return NotFound();
            }

            var campo = await _context.Campos
                .Include(c => c.Tipo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campo == null)
            {
                return NotFound();
            }

            return View(campo);
        }

        // POST: Campo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Campos == null)
            {
                return Problem("Entity set 'AppDbContext.Campos'  is null.");
            }
            var campo = await _context.Campos.FindAsync(id);
            if (campo != null)
            {
                _context.Campos.Remove(campo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampoExists(int id)
        {
          return (_context.Campos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
