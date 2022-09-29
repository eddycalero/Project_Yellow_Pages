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
    public class EmpresasController : Controller
    {
        private readonly YellowPagesContext _context;

        public EmpresasController(YellowPagesContext context)
        {
            _context = context;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            var yellowPagesContext = _context.Empresas.Include(e => e.Municipio);
            return View(await yellowPagesContext.ToListAsync());
        }

        // GET: Empresas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.Municipio)
                .FirstOrDefaultAsync(m => m.EmpresaId == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "MunicipioId");
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MunicipioId,Name,Description,Images1,DateCreate,IsActive,DireccionWeb,DescripcionTwo,ImagenTwo,Direccion")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                empresa.EmpresaId = Guid.NewGuid();
                _context.Add(empresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "MunicipioId", empresa.MunicipioId);
            return View(empresa);
        }

        // GET: Empresas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "MunicipioId", empresa.MunicipioId);
            return View(empresa);
        }

        // POST: Empresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmpresaId,MunicipioId,Name,Description,Images1,DateCreate,IsActive,DireccionWeb,DescripcionTwo,ImagenTwo,Direccion")] Empresa empresa)
        {
            if (id != empresa.EmpresaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.EmpresaId))
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
            ViewData["MunicipioId"] = new SelectList(_context.Municipios, "MunicipioId", "MunicipioId", empresa.MunicipioId);
            return View(empresa);
        }

        // GET: Empresas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Empresas == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .Include(e => e.Municipio)
                .FirstOrDefaultAsync(m => m.EmpresaId == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Empresas == null)
            {
                return Problem("Entity set 'YellowPagesContext.Empresas'  is null.");
            }
            var empresa = await _context.Empresas.FindAsync(id);
            if (empresa != null)
            {
                _context.Empresas.Remove(empresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(Guid id)
        {
          return _context.Empresas.Any(e => e.EmpresaId == id);
        }
    }
}
