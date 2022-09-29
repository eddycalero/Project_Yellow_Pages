using System;
using System.Collections.Generic;

namespace YellowPages.Entities
{
    public partial class ContactoEmpresa
    {
        public Guid ContactoId { get; set; }
        public Guid? EmpresaId { get; set; }
        public string ContactoName { get; set; } = null!;
        public bool? IsActive { get; set; }

        public virtual Empresa? Empresa { get; set; }
    }
}
