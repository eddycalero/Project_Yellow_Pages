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
    public class ContactoEmpresasController : Controller
    {
        private readonly YellowPagesContext _context;

        public ContactoEmpresasController(YellowPagesContext context)
        {
            _context = context;
        }

        // GET: ContactoEmpresas
        public async Task<IActionResult> Index()
        {
            var yellowPagesContext = _context.ContactoEmpresas.Include(c => c.Empresa);
            return View(await yellowPagesContext.ToListAsync());
        }

        // GET: ContactoEmpresas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ContactoEmpresas == null)
            {
                return NotFound();
            }

            var contactoEmpresa = await _context.ContactoEmpresas
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(m => m.ContactoId == id);
            if (contactoEmpresa == null)
            {
                return NotFound();
            }

            return View(contactoEmpresa);
        }

        // GET: ContactoEmpresas/Create
        public IActionResult Create()
        {
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId");
            return View();
        }

        // POST: ContactoEmpresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpresaId,ContactoName,IsActive")] ContactoEmpresa contactoEmpresa)
        {
            if (ModelState.IsValid)
            {
                contactoEmpresa.ContactoId = Guid.NewGuid();
                _context.Add(contactoEmpresa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", contactoEmpresa.EmpresaId);
            return View(contactoEmpresa);
        }

        // GET: ContactoEmpresas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ContactoEmpresas == null)
            {
                return NotFound();
            }

            var contactoEmpresa = await _context.ContactoEmpresas.FindAsync(id);
            if (contactoEmpresa == null)
            {
                return NotFound();
            }
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", contactoEmpresa.EmpresaId);
            return View(contactoEmpresa);
        }

        // POST: ContactoEmpresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ContactoId,EmpresaId,ContactoName,IsActive")] ContactoEmpresa contactoEmpresa)
        {
            if (id != contactoEmpresa.ContactoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactoEmpresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactoEmpresaExists(contactoEmpresa.ContactoId))
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
            ViewData["EmpresaId"] = new SelectList(_context.Empresas, "EmpresaId", "EmpresaId", contactoEmpresa.EmpresaId);
            return View(contactoEmpresa);
        }

        // GET: ContactoEmpresas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ContactoEmpresas == null)
            {
                return NotFound();
            }

            var contactoEmpresa = await _context.ContactoEmpresas
                .Include(c => c.Empresa)
                .FirstOrDefaultAsync(m => m.ContactoId == id);
            if (contactoEmpresa == null)
            {
                return NotFound();
            }

            return View(contactoEmpresa);
        }

        // POST: ContactoEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ContactoEmpresas == null)
            {
                return Problem("Entity set 'YellowPagesContext.ContactoEmpresas'  is null.");
            }
            var contactoEmpresa = await _context.ContactoEmpresas.FindAsync(id);
            if (contactoEmpresa != null)
            {
                _context.ContactoEmpresas.Remove(contactoEmpresa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactoEmpresaExists(Guid id)
        {
          return _context.ContactoEmpresas.Any(e => e.ContactoId == id);
        }
    }
}
