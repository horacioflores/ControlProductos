using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Newtonsoft.Json;

namespace ControlProductos.dataAccess
{
    public class tipoEmpaqueda : Base
    {
        public List<Entity.tipoEmpaque> GetCombo()
        {
            string json = methodGet("GetTipoEmpaques");
            Entity.GetTipoEmpaquesResult_ regreso = JsonConvert.DeserializeObject<Entity.GetTipoEmpaquesResult_>(json);
            return regreso.GetTipoEmpaquesResult;
        }
    }
}