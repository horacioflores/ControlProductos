using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetAplicacionesResult_
    {
        public List<Aplicacion> GetAplicacionesResult { get; set; }
    }
    public class GetCmbAplicacionResult_
    {
        public List<Aplicacion> GetCmbAplicacionResult { get; set; }
    }
    public class DelAplicacionResult_
    {
        public int DelAplicacionResult { get; set; }
    }
    public class InsAplicacionResult_
    {
        public int InsAplicacionResult { get; set; }
    }
    public class UpdAplicacionResult_
    {
        public int UpdAplicacionResult { get; set; }
    }

    //public class ValAplicacionResult_
    //{
    //    public int ValAplicacionResult { get; set; }
    //}

    public class DelAplicacionSelectedResult_
    {
        public int DelAplicacionSelectedResult { get; set; }
    }

    public class DelAplicacionAllResult_
    {
        public int DelAplicacionAllResult { get; set; }
    }

    public class Aplicacion
    {
        public int AppId { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string URL { get; set; }
        public bool Activo { get; set; }
        public string Imagen { get; set; }
        public string URLImagen { get; set; }

        public string CodigoYNombre { get; set; }
    }
}