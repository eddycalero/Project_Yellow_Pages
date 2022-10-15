using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YellowPages.Configuration;
using YellowPages.Entities;
using YellowPages.Models.ViewModel;

namespace YellowPages.Controllers
{
    [Authorize]
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
        [HttpPost]
        public  JsonResult GetFilterMunicipio(Guid id)
        {
            var muncipio = _context.Municipios.Where(x => x.DepartamentoEmpresaId == id).ToList();
            return Json(muncipio);
        }

        public async Task<IActionResult>Create()
        {
            ViewBag.Departamento = (await _context.DepartamentoEmpresas.ToListAsync()).Select(x =>
            new SelectListItem()
            {
                Text = x.Name,
                Value = x.DepartamentoEmpresaId.ToString()
            });
            return View();
        }

        // POST: Empresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpresaInsert e, IFormFile file1, IFormFile file2)
        {
           
                e.Images1 = await ConvercionImagen.ImagenConvertByte(file1).ConfigureAwait(false);
                e.ImagenTwo = await ConvercionImagen.ImagenConvertByte(file2).ConfigureAwait(false);
                await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC Jose.US_Empresa_Insert {e.Name},{e.Description}, {e.Images1},{e.DireccionWeb},{e.DescripcionTwo},{e.ImagenTwo},{e.Direccion},{e.Phone},{e.Correo},{e.NombreOferta},{e.NombreOferta2},{e.MunicipioID}");
                return RedirectToAction(nameof(Index));
            
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
        public async Task<IActionResult> Edit(Guid id, [Bind("EmpresaId,Name,Description,DateCreate,IsActive,DireccionWeb,DescripcionTwo,Direccion")] Empresa empresa)
        {
            if (id != empresa.EmpresaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var em = _context.Empresas.FirstOrDefault(x => x.EmpresaId == id);
                try
                {
                    em.Name = empresa.Name;
                    em.Description = empresa.Description;
                    em.DateCreate = empresa.DateCreate;
                    em.IsActive = empresa.IsActive;
                    em.DireccionWeb = empresa.DireccionWeb;
                    em.DescripcionTwo = empresa.DescripcionTwo;
                    em.Direccion = empresa.Direccion;
                    //_context.Update(empresa);
                    _context.Entry(em).State = EntityState.Modified;
                    
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
                empresa.IsActive = false;
                _context.Empresas.Update(empresa);
                await _context.SaveChangesAsync();
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
