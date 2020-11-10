using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetCompradoresResult_
    {
        public List<Comprador> GetCompradoresResult { get; set; }
    }
    public class GetCmbCompradoresResult_
    {
        public List<Comprador> GetCmbCompradoresResult { get; set; }
    }

    public class DelCompradorResult_
    {
        public int DelCompradorResult { get; set; }
    }
    public class InsCompradorResult_
    {
        public int InsCompradorResult { get; set; }
    }
    public class UpdCompradorResult_
    {
        public int UpdCompradorResult { get; set; }
    }
    public class ValCompradorResult_
    {
        public int ValCompradorResult { get; set; }
    }
    public class DelCompradorSelectedResult_
    {
        public int DelCompradorSelectedResult { get; set; }
    }
    public class DelCompradorAllResult_
    {
        public int DelCompradorAllResult { get; set; }
    }

    public class Comprador
    {
        public int CompradorID { get; set; }   
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string CodigoYNombre { get; set; }
    }
}