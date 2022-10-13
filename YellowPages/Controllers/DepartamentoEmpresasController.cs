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
    public class DepartamentoEmpresasController : Controller
    {
        private readonly YellowPagesContext _context;

        public DepartamentoEmpresasController(YellowPagesContext context)
        {
            _context = context;
        }

        // GET: DepartamentoEmpresas
        public async Task<IActionResult> Index()
        {
              return View(await _context.DepartamentoEmpresas.ToListAsync());
        }

        // GET: DepartamentoEmpresas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.DepartamentoEmpresas == null)
            {
                return NotFound();
            }

            var departamentoEmpresa = await _context.DepartamentoEmpresas
                .FirstOrDefaultAsync(m => m.DepartamentoEmpresaId == id);
            if (departamentoEmpresa == null)
            {
                return NotFound();
            }

            return View(departamentoEmpresa);
        }

        // GET: DepartamentoEmpresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DepartamentoEmpresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsActive")] DepartamentoEmpresa departamentoEmpresa)
        {
            if (ModelState.IsValid)
            {
                departamentoEmpresa.DepartamentoEmpresaId = Guid.NewGuid();
                _context.Add(departamentoEmpresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamentoEmpresa);
        }

        // GET: DepartamentoEmpresas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.DepartamentoEmpresas == null)
            {
                return NotFound();
            }

            var departamentoEmpresa = await _context.DepartamentoEmpresas.FindAsync(id);
            if (departamentoEmpresa == null)
            {
                return NotFound();
            }
            return View(departamentoEmpresa);
        }

        // POST: DepartamentoEmpresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DepartamentoEmpresaId,Name,IsActive")] DepartamentoEmpresa departamentoEmpresa)
        {
            if (id != departamentoEmpresa.DepartamentoEmpresaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamentoEmpresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentoEmpresaExists(departamentoEmpresa.DepartamentoEmpresaId))
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
            return View(departamentoEmpresa);
        }

        // GET: DepartamentoEmpresas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.DepartamentoEmpresas == null)
            {
                return NotFound();
            }

            var departamentoEmpresa = await _context.DepartamentoEmpresas
                .FirstOrDefaultAsync(m => m.DepartamentoEmpresaId == id);
            if (departamentoEmpresa == null)
            {
                return NotFound();
            }

            return View(departamentoEmpresa);
        }

        // POST: DepartamentoEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.DepartamentoEmpresas == null)
            {
                return Problem("Entity set 'YellowPagesContext.DepartamentoEmpresas'  is null.");
            }
            var departamentoEmpresa = await _context.DepartamentoEmpresas.FindAsync(id);
            if (departamentoEmpresa != null)
            {
                _context.DepartamentoEmpresas.Remove(departamentoEmpresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentoEmpresaExists(Guid id)
        {
          return _context.DepartamentoEmpresas.Any(e => e.DepartamentoEmpresaId == id);
        }
    }
}
