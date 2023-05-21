using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Formularios.Controllers
{
    public class FormularioController : Controller
    {
        private readonly AppDbContext _context;

        public FormularioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Formulario
        public async Task<IActionResult> Index()
        {
            return _context.Formularios != null ?
                        View(await _context.Formularios.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Formularios'  is null.");
        }

        // GET: Formulario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Formularios == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios
                .Include(form => form.Campos)
                .ThenInclude(Campo => Campo.Tipo)
                .FirstAsync(m => m.Id == id);


            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // GET: Formulario/Create
        public IActionResult Create()
        {
            ViewBag.Campos = new SelectList(_context.Campos, "Id", "Nome");
            return View();
        }

        // POST: Formulario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Formulario formulario)
        {
            var CamposForm = Request.Form["camposForm"];
            int[] CampoIds = CamposForm.ToString().Split(",").Select(Int32.Parse).ToArray();
            var Campos = _context.Campos.Where(campo => CampoIds.Contains(campo.Id)).ToList();
            formulario.Campos = new List<Campo>();
            formulario.Campos.AddRange(Campos);
            if (ModelState.IsValid)
            {
                _context.Add(formulario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Campos = new SelectList(_context.Campos, "Id", "Nome");
            return View(formulario);
        }

        // GET: Formulario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Formularios == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios
            .Include(form => form.Campos)
            .FirstAsync(form => form.Id == id);

            if (formulario == null)
            {
                return NotFound();
            }
            if (formulario.Campos is not null)
                ViewBag.Campos = formulario.Campos.ToList();
            ViewBag.CamposSelect = new SelectList(_context.Campos, "Id", "Nome");

            return View(formulario);
        }

        // POST: Formulario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Formulario formulario)
        {
            if (id != formulario.Id)
            {
                return NotFound();
            }

            int[] CampoIds = { };

            var CamposForm = Request.Form["camposForm"];
            try
            {
                CampoIds = CamposForm.ToString().Split(",").Select(Int32.Parse).ToArray();
            }
            catch (Exception e) { Console.Write(e.Message); }

            var Campos = new HashSet<Campo>(_context.Campos
            .Where(campo => CampoIds.Contains(campo.Id))
            ).ToHashSet();

            formulario.Campos = new List<Campo>();
            formulario.Campos.AddRange(Campos);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formulario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormularioExists(formulario.Id))
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
            return View(formulario);
        }

        // GET: Formulario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Formularios == null)
            {
                return NotFound();
            }

            var formulario = await _context.Formularios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (formulario == null)
            {
                return NotFound();
            }

            return View(formulario);
        }

        // POST: Formulario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Formularios == null)
            {
                return Problem("Entity set 'AppDbContext.Formularios'  is null.");
            }
            var formulario = await _context.Formularios.FindAsync(id);
            if (formulario != null)
            {
                _context.Formularios.Remove(formulario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormularioExists(int id)
        {
            return (_context.Formularios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
