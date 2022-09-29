using System;
using System.Collections.Generic;

namespace YellowPages.Entities
{
    public partial class DepartamentoEmpresa
    {
        public DepartamentoEmpresa()
        {
            Municipios = new HashSet<Municipio>();
        }

        public Guid DepartamentoEmpresaId { get; set; }
        public string Name { get; set; } = null!;
        public string SucursalName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual ICollection<Municipio> Municipios { get; set; }
    }
}
