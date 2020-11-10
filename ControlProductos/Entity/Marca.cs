using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace ControlProductos.Entity
{
    public class GetMarcasResult_
    {
        public List<Marca> GetMarcasResult { get; set; }
    }
    public class GetCmbMarcasResult_
    {
        public List<Marca> GetCmbMarcasResult { get; set; }
    }

    public class DelMarcaResult_
    {
        public int DelMarcaResult { get; set; }
    }
    public class InsMarcaResult_
    {
        public int InsMarcaResult { get; set; }
    }
    public class UpdMarcaResult_
    {
        public int UpdMarcaResult { get; set; }
    }
    public class ValMarcaResult_
    {
        public int ValMarcaResult { get; set; }
    }
    public class DelMarcaSelectedResult_
    {
        public int DelMarcaSelectedResult { get; set; }
    }
    public class DelMarcaAllResult_
    {
        public int DelMarcaAllResult { get; set; }
    }

    public class Marca
    {
        public int MarcaID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoYNombre { get; set; }
    }
}