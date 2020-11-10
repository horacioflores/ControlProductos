using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ControlProductos.Entity
{
    public class GetUMedidasResult_
    {
        public List<UMedida> GetUMedidasResult { get; set; }
    }
    public class GetCmbUMedidaResult_
    {
        public List<UMedida> GetCmbUMedidaResult { get; set; }
    }

    public class DelUmedidaResult_
    {
        public int DelUmedidaResult { get; set; }
    }
    public class InsUmedidaResult_
    {
        public int InsUmedidaResult { get; set; }
    }
    public class UpdUmedidaResult_
    {
        public int UpdUmedidaResult { get; set; }
    }
    public class ValUmedidaResult_
    {
        public int ValUmedidaResult { get; set; }
    }
    public class DelUmedidaSelectedResult_
    {
        public int DelUmedidaSelectedResult { get; set; }
    }
    public class DelUmedidaAllResult_
    {
        public int DelUmedidaAllResult { get; set; }
    }

    public class UMedida
    {
        public int UMedidaID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public string CodigoYNombre { get; set; }
    }
}