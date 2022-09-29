using System;
using System.Collections.Generic;

namespace YellowPages.Entities
{
    public partial class OfertaEmpresa
    {
        public Guid OfertaEmpresaId { get; set; }
        public Guid? EmpresaId { get; set; }
        public string NombreOferta { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual Empresa? Empresa { get; set; }
    }
}
