using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetJDETwosResult_
    {
        public List<JDETwo> GetJDETwosResult { get; set; }
    }

    public class JDETwo
    {
        public int JDEID { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
    }
}