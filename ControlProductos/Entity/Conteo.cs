using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetConteosResult_
    {
        public List<Conteo> GetConteosResult { get; set; }
    }
    public class GetCmbConteosResult_
    {
        public List<Conteo> GetCmbConteosResult { get; set; }
    }

    public class DelConteoResult_
    {
        public int DelConteoResult { get; set; }
    }
    public class InsConteoResult_
    {
        public int InsConteoResult { get; set; }
    }
    public class UpdConteoResult_
    {
        public int UpdConteoResult { get; set; }
    }
    public class ValConteoResult_
    {
        public int ValConteoResult { get; set; }
    }
    public class DelConteoSelectedResult_
    {
        public int DelConteoSelectedResult { get; set; }
    }
    public class DelConteoAllResult_
    {
        public int DelConteoAllResult { get; set; }
    }

    public class Conteo
    {
        public int ConteoID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoYNombre { get; set; }
    }
}