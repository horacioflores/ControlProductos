using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetTiposArticuloResult_
    {
        public List<TipoArticulo> GetTiposArticuloResult { get; set; }
    }
    public class GetCmbTiposArticuloResult_
    {
        public List<TipoArticulo> GetCmbTiposArticuloResult { get; set; }
    }
    public class DelTipoArticuloResult_
    {
        public int DelTipoArticuloResult { get; set; }
    }
    public class InsTipoArticuloResult_
    {
        public int InsTipoArticuloResult { get; set; }
    }
    public class UpdTipoArticuloResult_
    {
        public int UpdTipoArticuloResult { get; set; }
    }
    public class ValTipoArticuloResult_
    {
        public int ValTipoArticuloResult { get; set; }
    }
    public class DelTipoArticuloSelectedResult_
    {
        public int DelTipoArticuloSelectedResult { get; set; }
    }
    public class DelTipoArticuloAllResult_
    {
        public int DelTipoArticuloAllResult { get; set; }
    }

    public class TipoArticulo
    {
        public int tipoArticuloID { get; set; }
        public string codigoTipoArticulo { get; set; }   
        public string tipoArticulo { get; set; }
        public string M { get; set; }
        public string N { get; set; }
        public string comentarios { get; set; }
        public string CodigoYNombre { get; set; }
    }
}