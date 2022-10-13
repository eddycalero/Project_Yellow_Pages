using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YellowPages.Entities;
using YellowPages.Models;
using Microsoft.EntityFrameworkCore;
using YellowPages.Models.ViewModel;

namespace YellowPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly YellowPagesContext _context;

        public HomeController(ILogger<HomeController> logger, YellowPagesContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ActionResult> ByteConvertImagen(Guid id)
        {
            var imagen = await _context.Empresas.Where(x => x.EmpresaId == id)
                                    .Select(x => x.Images1).FirstOrDefaultAsync();
            return File(imagen, "Imagen/jpg");
        }
        public async Task<ActionResult> ByteConvertImagen2(Guid id)
        {
            var imagen = await _context.Empresas.Where(x => x.EmpresaId == id)
                                    .Select(x => x.Images1).FirstOrDefaultAsync();
            return File(imagen, "Imagen/jpg");
        }

        public IActionResult Index()
        {
            AnuncioEmpresa anuncio = new AnuncioEmpresa();
            var Result = _context.AnuncioEmpresa.FromSqlInterpolated($"EXEC Jose.US_AnuncioEmpresa").ToList();
         
            return View(Result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}