using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetfamiliasResult_
    {
        public List<familia> GetfamiliasResult { get; set; }
    }

    public class familia
    {
        public int familiaID { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}