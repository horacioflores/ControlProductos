using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetPlaneadoresResult_
    {
        public List<Planeador> GetPlaneadoresResult { get; set; }
    }
    public class GetCmbPlaneadoresResult_
    {
        public List<Planeador> GetCmbPlaneadoresResult { get; set; }
    }

    public class DelPlaneadorResult_
    {
        public int DelPlaneadorResult { get; set; }
    }
    public class InsPlaneadorResult_
    {
        public int InsPlaneadorResult { get; set; }
    }
    public class UpdPlaneadorResult_
    {
        public int UpdPlaneadorResult { get; set; }
    }
    public class ValPlaneadorResult_
    {
        public int ValPlaneadorResult { get; set; }
    }
    public class DelPlaneadorSelectedResult_
    {
        public int DelPlaneadorSelectedResult { get; set; }
    }
    public class DelPlaneadorAllResult_
    {
        public int DelPlaneadorAllResult { get; set; }
    }

    public class Planeador
    {
        public int PlaneadorID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string CodigoYNombre { get; set; }
    }
}