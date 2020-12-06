using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class ControlProductosda : Base
    {
        public List<Entity.ControlProductos> GetCtrlProd(string fechaini, string fechafin, string folio)
        {
            string json = methodGet("GetCtrlProd/" + fechaini + "/" + fechafin + "/" + folio);
            Entity.GetCtrlProdResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProdResult_>(json);
            return regreso.GetCtrlProdResult;
        }

        public List<Entity.ControlProductos> GetCtrlProducto(string ctrlProdsID)
        {
            string json = methodGet("GetCtrlProducto/" + ctrlProdsID);
            Entity.GetCtrlProductoResult_ regreso = JsonConvert.DeserializeObject<Entity.GetCtrlProductoResult_>(json);
            return regreso.GetCtrlProductoResult;
        }

        public int InsCtrlP(Entity.ControlProductos ctrlProds, string UsuarioId)
        {
            int regreso = Convert.ToInt32(methodPost("InsCtrlP/" + UsuarioId, JsonConvert.SerializeObject(ctrlProds)));
            return regreso;
        }

        public int UpdCtrlP(Entity.ControlProductos ctrlProds, string UsuarioId)
        {
            int regreso = Convert.ToInt32(methodPost("UpdCtrlP/" + UsuarioId, JsonConvert.SerializeObject(ctrlProds)));
            return regreso;
        }
    }
}