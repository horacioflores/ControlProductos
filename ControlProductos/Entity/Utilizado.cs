using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetUtilizadosResult_
    {
        public List<Utilizado> GetUtilizadosResult { get; set; }
    }
    public class GetCmbUtilizadosResult_
    {
        public List<Utilizado> GetCmbUtilizadosResult { get; set; }
    }

    public class DelUtilizadoResult_
    {
        public int DelUtilizadoResult { get; set; }
    }
    public class InsUtilizadoResult_
    {
        public int InsUtilizadoResult { get; set; }
    }
    public class UpdUtilizadoResult_
    {
        public int UpdUtilizadoResult { get; set; }
    }
    public class ValUtilizadoResult_
    {
        public int ValUtilizadoResult { get; set; }
    }
    public class DelUtilizadoSelectedResult_
    {
        public int DelUtilizadoSelectedResult { get; set; }
    }
    public class DelUtilizadoAllResult_
    {
        public int DelUtilizadoAllResult { get; set; }
    }

    public class Utilizado
    {
        public int UtilizadoID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoYNombre { get; set; }
    }
}