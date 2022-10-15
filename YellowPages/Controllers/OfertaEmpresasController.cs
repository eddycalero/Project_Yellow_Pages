using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YellowPages.Entities;

namespace YellowPages.Controllers
{
    [Authorize]
    public class OfertaEmpresasController : Controller
    {
        private readonly YellowPagesContext _context;

        public OfertaEmpresasController(YellowPagesContext context)
        {
            _context = context;
        }

        // GET: OfertaEmpresas
        public async Task<IActionResult> Index()
        {
            var yellowPagesContext = _context.OfertaEmpresas.Include(o => o.Empresa);
            return View(await yellowPagesContext.ToListAsync());
        }

        // GET: OfertaEmpresas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.OfertaEmpresas == null)
            {
                return NotFound();
            }

            var ofertaEmpresa = await _context.OfertaEmpresas
                .Include(o => o.Empresa)
                .FirstOrDefaultAsync(m => m.OfertaEmpresaId == id);
            if (ofertaEmpresa == null)
            {
                return NotFound();
            }

            return View(ofertaEmpresa);
        }

        // GET: OfertaEmpresas/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId");
            return View();
        }

        // POST: OfertaEmpresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpresaId,NombreOferta,IsActive")] OfertaEmpresa ofertaEmpresa)
        {
            if (ModelState.IsValid)
            {
                ofertaEmpresa.OfertaEmpresaId = Guid.NewGuid();
                _context.Add(ofertaEmpresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", ofertaEmpresa.EmpresaId);
            return View(ofertaEmpresa);
        }

        // GET: OfertaEmpresas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.OfertaEmpresas == null)
            {
                return NotFound();
            }

            var ofertaEmpresa = await _context.OfertaEmpresas.FindAsync(id);
            if (ofertaEmpresa == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", ofertaEmpresa.EmpresaId);
            return View(ofertaEmpresa);
        }

        // POST: OfertaEmpresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("OfertaEmpresaId,EmpresaId,NombreOferta,IsActive")] OfertaEmpresa ofertaEmpresa)
        {
            if (id != ofertaEmpresa.OfertaEmpresaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ofertaEmpresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfertaEmpresaExists(ofertaEmpresa.OfertaEmpresaId))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", ofertaEmpresa.EmpresaId);
            return View(ofertaEmpresa);
        }

        // GET: OfertaEmpresas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.OfertaEmpresas == null)
            {
                return NotFound();
            }

            var ofertaEmpresa = await _context.OfertaEmpresas
                .Include(o => o.Empresa)
                .FirstOrDefaultAsync(m => m.OfertaEmpresaId == id);
            if (ofertaEmpresa == null)
            {
                return NotFound();
            }

            return View(ofertaEmpresa);
        }

        // POST: OfertaEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.OfertaEmpresas == null)
            {
                return Problem("Entity set 'YellowPagesContext.OfertaEmpresas'  is null.");
            }
            var ofertaEmpresa = await _context.OfertaEmpresas.FindAsync(id);
            if (ofertaEmpresa != null)
            {
                _context.OfertaEmpresas.Remove(ofertaEmpresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfertaEmpresaExists(Guid id)
        {
          return _context.OfertaEmpresas.Any(e => e.OfertaEmpresaId == id);
        }
    }
}
