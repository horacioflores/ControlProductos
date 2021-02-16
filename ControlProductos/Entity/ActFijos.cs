using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetActFijosResult_
    {
        public List<ActFijos> GetActFijosResult { get; set; }
    }

    public class ActFijos
    {
        public int ActFijosID { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}