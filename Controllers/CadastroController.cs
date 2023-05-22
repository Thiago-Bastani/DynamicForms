using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Formularios.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppDbContext _context;

        public CadastroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Cadastro
        public async Task<IActionResult> Index()
        {
            return _context.Registros != null ?
                        View(await _context.Registros.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Registros'  is null.");
        }

        // GET: Cadastro/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Registros == null)
            {
                return NotFound();
            }

            var registroViewModel = await _context.Registros
                .Include(reg => reg.RegInfo)
                .ThenInclude(regInfo => regInfo.Campo)
                .FirstAsync(m => m.Id == id);

            if (registroViewModel == null)
            {
                return NotFound();
            }

            return View(registroViewModel);
        }

        // GET: Cadastro/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Registros == null)
            {
                return NotFound();
            }

            var registroViewModel = await _context.Registros.FindAsync(id);
            if (registroViewModel == null)
            {
                return NotFound();
            }
            return View(registroViewModel);
        }

        // POST: Cadastro/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFormulario")] RegistroViewModel registroViewModel)
        {
            if (id != registroViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroViewModelExists(registroViewModel.Id))
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
            return View(registroViewModel);
        }

        // GET: Cadastro/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Registros == null)
            {
                return NotFound();
            }

            var registroViewModel = await _context.Registros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroViewModel == null)
            {
                return NotFound();
            }

            return View(registroViewModel);
        }

        // POST: Cadastro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Registros == null)
            {
                return Problem("Entity set 'AppDbContext.Registros'  is null.");
            }
            var registroViewModel = await _context.Registros.FindAsync(id);
            if (registroViewModel != null)
            {
                _context.Registros.Remove(registroViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroViewModelExists(int id)
        {
            return (_context.Registros?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
