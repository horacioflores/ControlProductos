using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetdiasResult_
    {
        public List<dias> GetdiasResult { get; set; }
    }

    public class dias
    {
        public int diasID { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}