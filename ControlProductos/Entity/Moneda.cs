using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetMonedasResult_
    {
        public List<Moneda> GetMonedasResult { get; set; }
    }
    public class GetCmbMonedaResult_
    {
        public List<Moneda> GetCmbMonedaResult { get; set; }
    }

    public class DelMonedaResult_
    {
        public int DelMonedaResult { get; set; }
    }
    public class InsMonedaResult_
    {
        public int InsMonedaResult { get; set; }
    }
    public class UpdMonedaResult_
    {
        public int UpdMonedaResult { get; set; }
    }
    public class ValMonedaResult_
    {
        public int ValMonedaResult { get; set; }
    }
    public class DelMonedaSelectedResult_
    {
        public int DelMonedaSelectedResult { get; set; }
    }
    public class DelMonedaAllResult_
    {
        public int DelMonedaAllResult { get; set; }
    }

    public class Moneda
    {
        public int MonedaID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
        public decimal precs { get; set; }
        public string sep_millar { get; set; }
        public string sep_decimal { get; set; }
        public bool Activo { get; set; }
        public string CodigoYNombre { get; set; }
    }
}