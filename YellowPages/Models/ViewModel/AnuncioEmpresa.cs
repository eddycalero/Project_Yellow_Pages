using Microsoft.EntityFrameworkCore;

namespace YellowPages.Models.ViewModel

{
	[Keyless]
	public class AnuncioEmpresa
	{
		public Guid?  EmpresaID { get; set; }
		public string? Departamento { get; set; }
		public string? Municipio { get; set; }
		public string? Empresa { get; set; }
		public string? Description { get; set; }
		public string? DireccionWeb { get; set; }
		public string? DescripcionTwo { get; set; }
		public string? Direccion { get; set; }



	}

	public class AnuncioViewEmpres : AnuncioEmpresa
	{
		public string? Contacto1 { get; set; }
		public string? Contacto2 { get; set; }

	}

}
