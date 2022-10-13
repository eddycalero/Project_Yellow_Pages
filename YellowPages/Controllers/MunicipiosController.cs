using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YellowPages.Entities;

namespace YellowPages.Controllers
{
    public class MunicipiosController : Controller
    {
        private readonly YellowPagesContext _context;

        public MunicipiosController(YellowPagesContext context)
        {
            _context = context;
        }

        // GET: Municipios
        public async Task<IActionResult> Index()
        {
            var yellowPagesContext = _context.Municipios.Include(m => m.DepartamentoEmpresa);
            return View(await yellowPagesContext.ToListAsync());
        }

        // GET: Municipios/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Municipios == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipios
                .Include(m => m.DepartamentoEmpresa)
                .FirstOrDefaultAsync(m => m.MunicipioId == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // GET: Municipios/Create
        public IActionResult Create()
        {
            ViewData["DepartamentoEmpresaId"] = new SelectList(_context.DepartamentoEmpresas, "DepartamentoEmpresaId", "DepartamentoEmpresaId");

            return View();
        }

        // POST: Municipios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartamentoEmpresaId,Name,IsActive")] Municipio municipio)
        {
            if (ModelState.IsValid)
            {
                municipio.MunicipioId = Guid.NewGuid();
                _context.Add(municipio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoEmpresaId"] = new SelectList(_context.DepartamentoEmpresas, "DepartamentoEmpresaId", "DepartamentoEmpresaId", municipio.DepartamentoEmpresaId);
            return View(municipio);
        }

        // GET: Municipios/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Municipios == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipios.FindAsync(id);
            if (municipio == null)
            {
                return NotFound();
            }
            ViewData["DepartamentoEmpresaId"] = new SelectList(_context.DepartamentoEmpresas, "DepartamentoEmpresaId", "DepartamentoEmpresaId", municipio.DepartamentoEmpresaId);
            return View(municipio);
        }

        // POST: Municipios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MunicipioId,DepartamentoEmpresaId,Name,IsActive")] Municipio municipio)
        {
            if (id != municipio.MunicipioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(municipio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MunicipioExists(municipio.MunicipioId))
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
            ViewData["DepartamentoEmpresaId"] = new SelectList(_context.DepartamentoEmpresas, "DepartamentoEmpresaId", "DepartamentoEmpresaId", municipio.DepartamentoEmpresaId);
            return View(municipio);
        }

        // GET: Municipios/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Municipios == null)
            {
                return NotFound();
            }

            var municipio = await _context.Municipios
                .Include(m => m.DepartamentoEmpresa)
                .FirstOrDefaultAsync(m => m.MunicipioId == id);
            if (municipio == null)
            {
                return NotFound();
            }

            return View(municipio);
        }

        // POST: Municipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Municipios == null)
            {
                return Problem("Entity set 'YellowPagesContext.Municipios'  is null.");
            }
            var municipio = await _context.Municipios.FindAsync(id);
            if (municipio != null)
            {
                _context.Municipios.Remove(municipio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MunicipioExists(Guid id)
        {
          return _context.Municipios.Any(e => e.MunicipioId == id);
        }
    }
}
