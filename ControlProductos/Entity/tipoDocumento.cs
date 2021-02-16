using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetTiposDocumentosResult_
    {
        public List<tipoDocumento> GetTiposDocumentosResult { get; set; }
    }

    public class tipoDocumento
    {
        public int tipoDocumentoID { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}