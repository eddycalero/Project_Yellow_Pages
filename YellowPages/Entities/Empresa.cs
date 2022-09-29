using System;
using System.Collections.Generic;

namespace YellowPages.Entities
{
    public partial class Empresa
    {
        public Empresa()
        {
            ContactoEmpresas = new HashSet<ContactoEmpresa>();
            OfertaEmpresas = new HashSet<OfertaEmpresa>();
        }

        public Guid EmpresaId { get; set; }
        public Guid? MunicipioId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public byte[]? Images1 { get; set; }
        public DateTime DateCreate { get; set; }
        public bool? IsActive { get; set; }
        public string DireccionWeb { get; set; } = null!;
        public string DescripcionTwo { get; set; } = null!;
        public byte[]? ImagenTwo { get; set; }
        public string Direccion { get; set; } = null!;

        public virtual Municipio? Municipio { get; set; }
        public virtual ICollection<ContactoEmpresa> ContactoEmpresas { get; set; }
        public virtual ICollection<OfertaEmpresa> OfertaEmpresas { get; set; }
    }
}
