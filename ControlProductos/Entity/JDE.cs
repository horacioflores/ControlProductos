using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetJDEsResult_
    {
        public List<JDE> GetJDEsResult { get; set; }
    }

    public class JDE
    {
        public int JDEID { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}