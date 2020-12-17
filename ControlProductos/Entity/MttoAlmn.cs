using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetMttoAlmnResult_
    {
        public List<MttoAlmn> GetMttoAlmnResult { get; set; }
    }
    public class GetCmbMttoAlmnResult_
    {
        public List<MttoAlmn> GetCmbMttoAlmnResult { get; set; }
    }
    public class DelMttoAlmnResult_
    {
        public int DelMttoAlmnResult { get; set; }
    }
    public class InsMttoAlmnResult_
    {
        public int InsMttoAlmnResult { get; set; }
    }
    public class UpdMttoAlmnResult_
    {
        public int UpdMttoAlmnResult { get; set; }
    }
    public class ValMttoAlmnResult_
    {
        public int ValMttoAlmnResult { get; set; }
    }
    public class DelMttoAlmnSelectedResult_
    {
        public int DelMttoAlmnSelectedResult { get; set; }
    }
    public class DelMttoAlmnAllResult_
    {
        public int DelMttoAlmnAllResult { get; set; }
    }

    public class MttoAlmn
    {
        public int MttoAlmnID { get; set; }
        public string codigoMttoAlmn { get; set; }
        public string especificacion { get; set; }
        public string notas { get; set; }
        public string clasificacion { get; set; }
        public string responsable { get; set; }
        public string tipo { get; set; }
        public string tipo_s { get; set; }
    }
}