using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetParametroResult_
    {
        public Parametro GetParametroResult { get; set; }
    }
    public class GetParametrosResult_
    {
        public List<Parametro> GetParametrosResult { get; set; }
    }
    public class UpdParametroResult_
    {
        public int UpdParametroResult;
    }

    public class Parametro
    {
        public int ID { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public int PadreId { get; set; }
        public string Padre { get; set; }
        public bool Editable { get; set; }
        public bool Encriptado { get; set; }
        public string ClaveYDescripcion { get; set; }
    }
}