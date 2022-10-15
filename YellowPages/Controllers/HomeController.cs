using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YellowPages.Entities;
using YellowPages.Models;
using Microsoft.EntityFrameworkCore;
using YellowPages.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

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
                                    .Select(x => x.ImagenTwo).FirstOrDefaultAsync();
            return File(imagen, "Imagen/jpg");
        }

        public async Task GetNameContacto(Guid id){

            var contacto = await _context.ContactoEmpresas.Where(x => x.EmpresaId == id).FirstOrDefaultAsync();
            ViewBag.phone = contacto;
        }

        public IActionResult Index()
        {
          
            List<AnuncioViewEmpres> list = new List<AnuncioViewEmpres>();
             List<AnuncioEmpresa> Result = _context.AnuncioEmpresa.FromSqlInterpolated($"EXEC Jose.US_AnuncioEmpresa").ToList();
			foreach (var item in Result)
			{
                AnuncioViewEmpres a = new AnuncioViewEmpres();
                a.EmpresaID = item.EmpresaID;
                a.Departamento = item.Departamento;
                a.Municipio = item.Municipio;
                a.Empresa = item.Empresa;
                a.Description = item.Description;
                a.DireccionWeb = item.DireccionWeb;
                a.DescripcionTwo = item.DescripcionTwo;
                a.Direccion = item.Direccion;
                a.Contacto1 = _context.ContactoEmpresas.Where(x => x.EmpresaId == item.EmpresaID).Select(
                x => x.ContactoName).FirstOrDefault();
				a.Contacto2 = _context.ContactoEmpresas.Where(x => x.EmpresaId == item.EmpresaID).OrderByDescending
				(x => x.ContactoId).Select(
				x => x.ContactoName).FirstOrDefault();
                list.Add(a);
            }
            
            return View(list);
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