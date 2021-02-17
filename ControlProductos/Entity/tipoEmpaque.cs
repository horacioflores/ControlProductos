using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetTipoEmpaquesResult_
    {
        public List<tipoEmpaque> GetTipoEmpaquesResult { get; set; }
    }

    public class tipoEmpaque
    {
        public int tipoEmpaqueID { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}