using System;
using System.Collections.Generic;

namespace YellowPages.Entities
{
    public partial class Municipio
    {
        public Municipio()
        {
            Empresas = new HashSet<Empresa>();
        }

        public Guid MunicipioId { get; set; }
        public Guid? DepartamentoEmpresaId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual DepartamentoEmpresa? DepartamentoEmpresa { get; set; }
        public virtual ICollection<Empresa> Empresas { get; set; }
    }
}
